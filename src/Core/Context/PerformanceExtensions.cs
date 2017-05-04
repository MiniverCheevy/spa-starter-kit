using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Voodoo;

namespace Fernweh.Core.Context
{
    //TODO: consider creating a facade that wraps all of this and having the repository expose it
    //TODO: consider consistent naming, bulk insert, bulk update and coming one day bulk delete
    public class BulkCopyRequest
    {
        public BulkCopyRequest()
        {
            ExclusionList = new List<string>();
        }

        public BulkCopyRequest(string tableName, SqlConnection connection, List<string> exclusionList = null)
        {
            TableName = tableName;
            Connection = connection;

            ExclusionList = exclusionList ?? new List<string>();
        }

        public string TableName { get; set; }
        public SqlConnection Connection { get; set; }
        public List<string> ExclusionList { get; set; }
    }

    public static class PerformanceExtensions
    {
        private static DataTable toDataTable<T>(this IEnumerable<T> value, List<string> exclusionList)
            where T : class
        {
            var dataTable = new DataTable();

            var type = typeof(T);

            var properties =
                type.GetProperties().Where(x => x.PropertyType.IsScalar() && !exclusionList.Contains(x.Name)).ToList();

            foreach (var propertyInfo in properties)
            {
                var propertyType = propertyInfo.PropertyType;
                if (!propertyType.IsScalar())
                    continue;

                var nullableType = Nullable.GetUnderlyingType(propertyType);
                propertyType = nullableType ?? propertyType;

                var dataColumn = new DataColumn(propertyInfo.Name, propertyType);

                if (nullableType != null)
                    dataColumn.AllowDBNull = true;

                dataTable.Columns.Add(dataColumn);
            }

            foreach (var row in value)
            {
                var dataRow = dataTable.NewRow();

                foreach (var property in properties)
                    dataRow[property.Name] = SqlBuilder<T>.Sqlize(property.GetValue(row));

                dataTable.Rows.Add(dataRow);
            }

            return dataTable;
        }

        public static void BulkCopy(this DataTable dataTable, BulkCopyRequest bulkCopyRequest)
        {
            if (bulkCopyRequest.Connection.State != ConnectionState.Open)
                bulkCopyRequest.Connection.Open();

            using (var sqlBulkCopy = new SqlBulkCopy(bulkCopyRequest.Connection))
            {
                sqlBulkCopy.DestinationTableName = bulkCopyRequest.TableName;

                foreach (var dc in dataTable.Columns)
                    sqlBulkCopy.ColumnMappings.Add(((DataColumn) dc).ColumnName, ((DataColumn) dc).ColumnName);

                sqlBulkCopy.WriteToServer(dataTable);
            }
        }

        public static void BulkCopy<T>(this IEnumerable<T> source, BulkCopyRequest bulkCopyRequest)
            where T : class
        {
            source = source.ToList();
            var dataTable = source.toDataTable(bulkCopyRequest.ExclusionList);
            dataTable.BulkCopy(bulkCopyRequest);
        }

        public static int Update<T>(this DbContext context, SqlUpdater<T> update)
        {
            int result = 0;
            var cnn = context.Database.Connection;

            using (var cmd = new SqlCommand(update.ToString(), cnn.To<SqlConnection>()))
            {
                foreach (var param in update.Parameters)
                {
                    cmd.Parameters.AddWithValue(param.Key, param.Value);
                }
                result = cmd.ExecuteNonQuery();
            }

            return result;
        }

        public static int Delete<T>(this DbContext context, SqlDeleter<T> update)
        {
            int result = 0;
            var cnn = context.Database.Connection;

            cnn.Open();
            using (var cmd = new SqlCommand(update.ToString(), cnn.To<SqlConnection>()))
            {
                foreach (var param in update.Parameters)
                {
                    cmd.Parameters.AddWithValue(param.Key, param.Value);
                }
                result = cmd.ExecuteNonQuery();
            }

            return result;
        }
    }

    public class SqlDeleter<T> : SqlBuilder<T>
    {
        public SqlDeleter(TableInformation tableInformation, SqlStrings target)
            : base(tableInformation, target)
        {
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("DELETE {0}", Prefix);
            var last = Target.SetStatements.Any() ? Target.SetStatements.Last() : null;

            foreach (var setClause in Target.SetStatements)
            {
                sb.AppendLine(setClause);
                if (setClause != last && last != null)
                    sb.Append(",");
            }
            sb.AppendLine("");
            sb.AppendFormat("FROM [{0}].[{1}] {2}", SqlSchema, SqlTableName, Prefix);
            if (Target.SqlJoinClauses.Any())
            {
                foreach (var join in Target.SqlJoinClauses)
                    sb.AppendLine(join);
            }
            sb.Append(GetWhereClause());
            return sb.ToString();
        }
    }

    public class SqlUpdater<TModel> : SqlBuilder<TModel>
    {
        public SqlUpdater(TableInformation tableInformation, SqlStrings target)
            : base(tableInformation, target)
        {
        }

        public void Set<TProperty>(Expression<Func<TModel, TProperty>> propertyName, TProperty value)
        {
            string alias = GetFieldName(propertyName);

            Target.SetStatements.Add($"[{alias}] = {GetParameter(value)} ");
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("UPDATE [{0}].[{1}] SET ", SqlSchema, SqlTableName);
            var last = Target.SetStatements.Any() ? Target.SetStatements.Last() : null;

            foreach (var setClause in Target.SetStatements)
            {
                sb.AppendLine(setClause);
                if (setClause != last && last != null)
                    sb.Append(",");
            }

            if (Target.SqlJoinClauses.Any())
            {
                sb.AppendFormat(" FROM [{0}].[{1}] {2}", SqlSchema, SqlTableName, Prefix);

                foreach (var join in Target.SqlJoinClauses)
                    sb.AppendLine(join);
            }

            sb.Append(GetWhereClause());
            return sb.ToString();
        }

        public new string ToDebugString()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("SELECT * ");
            var last = Target.SetStatements.Any() ? Target.SetStatements.Last() : null;

            sb.AppendFormat(" FROM [{0}].[{1}] {2}", SqlSchema, SqlTableName, Prefix);
            if (Target.SqlJoinClauses.Any())
            {
                foreach (var join in Target.SqlJoinClauses)
                    sb.AppendLine(join);
            }
            var whereClause = GetWhereClause();
            var parameters = Parameters;
            parameters.Reverse();
            foreach (var param in parameters)
            {
                var value = SqlFormat(param.Value);
                whereClause = whereClause.Replace(param.Key, value);
            }
            sb.Append(whereClause);
            return sb.ToString();
        }
    }

    public abstract class SqlStrings
    {
        public SqlOperation SqlOperation { get; set; }
        public List<string> SqlWhereClauses { get; set; }
        public List<string> SqlFromClauses { get; set; }
        public List<string> SqlJoinClauses { get; set; }
        public List<string> SetStatements { get; set; }

        public List<KeyValuePair<string, object>> Parameters { get; set; }
        public int SqlParameterCount = 0;

        protected SqlStrings()
        {
            SqlWhereClauses = new List<string>();
            SqlJoinClauses = new List<string>();
            SqlFromClauses = new List<string>();
            SetStatements = new List<string>();
            Parameters = new List<KeyValuePair<string, object>>();
        }
    }

    public class SqlUpateStrings : SqlStrings
    {
        public SqlUpateStrings() : base()
        {
            SqlOperation = SqlOperation.Update;
        }
    }

    public class SqlDeleteStrings : SqlStrings
    {
        public SqlDeleteStrings()
            : base()
        {
            SqlOperation = SqlOperation.Delete;
        }
    }

    public class TableInformation
    {
        public string Schema { get; set; }
        public string Table { get; set; }
    }

    public enum SqlOperation
    {
        Ancillary = 1,
        Update = 2,
        Delete = 3
    }

    public class SqlBuilder<TModel>
    {
        public List<KeyValuePair<string, object>> Parameters
        {
            get { return Target.Parameters; }
        }

        public string SqlSchema { get; set; }
        public string SqlTableName { get; set; }

        protected SqlStrings Target { get; set; }

        public Type Type
        {
            get { return typeof(TModel); }
        }

        protected string Prefix
        {
            get { return SqlTableName.ToLower(); }
        }

        protected string GetAliasedFieldName(string name, bool forceAliasing = false)
        {
            if (Target.SqlJoinClauses.Any() || Target.SqlOperation == SqlOperation.Delete || forceAliasing)
                return string.Format(" [{0}].[{1}] ", Prefix, name);

            return name;
        }

        public const string ParamaterFormat = "@p{0}";


        public SqlBuilder(TableInformation tableInformation, SqlStrings target)
        {
            SqlSchema = tableInformation.Schema;
            SqlTableName = tableInformation.Table;
            Target = target;
        }

        protected string GetWhereClause()
        {
            if (!Target.SqlWhereClauses.Any())
                return string.Empty;
            var sb = new StringBuilder();
            sb.AppendLine(" WHERE ");
            var last = Target.SqlWhereClauses.Any() ? Target.SqlWhereClauses.Last() : null;

            foreach (var clause in Target.SqlWhereClauses)
            {
                sb.AppendLine(clause);
                if (clause != last && last != null)
                    sb.Append(" AND ");
            }
            return sb.ToString();
        }

        public void WhereIn<TProperty>(Expression<Func<TModel, TProperty>> propertyName, params TProperty[] values)
        {
            var alias = GetFieldName(propertyName);

            var sb = new StringBuilder();
            sb.AppendLine("(");
            sb.Append(GetAliasedFieldName(alias));
            sb.Append(" IN (");
            sb.Append(GetSqlListFrom(values));
            sb.Append(") )");
            Target.SqlWhereClauses.Add(sb.ToString());
        }

        public void WhereNotIn<TProperty>(Expression<Func<TModel, TProperty>> propertyName, params TProperty[] values)
        {
            string name = GetFieldName(propertyName);

            var sb = new StringBuilder();
            sb.Append("(");
            sb.Append(GetAliasedFieldName(name));
            sb.Append(" NOT IN (");
            sb.Append(GetSqlListFrom(values));
            sb.Append(") )");
            Target.SqlWhereClauses.Add(sb.ToString());
        }

        public void WhereNotEqual<TProperty>(Expression<Func<TModel, TProperty>> propertyName, TProperty value)
        {
            var alias = GetFieldName(propertyName);
            var sqlValue = GetParameter(value);
            if (typeof(TProperty) == typeof(DateTime))
                buildDatePredicate<TProperty>(value, alias, "!=");
            else
                Target.SqlWhereClauses.Add(string.Format("[{0}].[{1}] != {2}", Prefix, alias, sqlValue));
        }

        private void buildDatePredicate<TProperty>(TProperty value, string alias, string comparison)
        {
            var date = value.To<DateTime>();
            var year = date.Year;
            var month = date.Month;
            var day = date.Day;

            Target.SqlWhereClauses.Add(string.Format("Year([{0}].[{1}]) {3} {2}", Prefix, alias, year, comparison));
            Target.SqlWhereClauses.Add(string.Format("Month([{0}].[{1}]) {3} {2}", Prefix, alias, month, comparison));
            Target.SqlWhereClauses.Add(string.Format("Day([{0}].[{1}]) {3} {2}", Prefix, alias, day, comparison));
        }

        public void WhereEqual<TProperty>(Expression<Func<TModel, TProperty>> propertyName, TProperty value)
        {
            var alias = GetFieldName(propertyName);
            var sqlValue = GetParameter(value);
            if (typeof(TProperty) == typeof(DateTime))
                buildDatePredicate(value, alias, "=");
            else
                Target.SqlWhereClauses.Add(string.Format("[{0}].[{1}] = {2}", Prefix, alias, sqlValue));
        }

        public void WhereGreaterThanOrEqualTo<TProperty>(Expression<Func<TModel, TProperty>> propertyName,
            TProperty value)
        {
            var alias = GetFieldName(propertyName);
            var sqlValue = GetParameter(value);

            Target.SqlWhereClauses.Add(string.Format("[{0}].[{1}] >= {2}", Prefix, alias, sqlValue));
        }

        public void WhereLessThanOrEqualTo<TProperty>(Expression<Func<TModel, TProperty>> propertyName, TProperty value)
        {
            var alias = GetFieldName(propertyName);
            var sqlValue = GetParameter(value);

            Target.SqlWhereClauses.Add(string.Format("[{0}].[{1}] <= {2}", Prefix, alias, sqlValue));
        }

        protected static string GetFieldName<TObject, TProperty>(Expression<Func<TObject, TProperty>> propertyName)
        {
            string name = string.Empty;
            if (propertyName.Body is MemberExpression)
                name = ((MemberExpression) propertyName.Body).Member.Name;
            else
            {
                var op = ((UnaryExpression) propertyName.Body).Operand;
                name = ((MemberExpression) op).Member.Name;
            }
            return name;
        }

        public void InnerJoin<TTargetEntity, TModelProperty, TTargetProperty>(
            SqlBuilder<TTargetEntity> targetEntity,
            Expression<Func<TModel, TModelProperty>> leftSide,
            Expression<Func<TTargetEntity, TTargetProperty>> rightSide)
        {
            var sb = new StringBuilder();
            sb.AppendLine("");
            sb.AppendFormat(" INNER JOIN [{0}].[{1}] {2}", targetEntity.SqlSchema, targetEntity.SqlTableName,
                targetEntity.Prefix);
            sb.AppendLine("");
            sb.AppendFormat(" ON {0} = {1} ", this.GetAliasedFieldName(GetFieldName(leftSide), true),
                targetEntity.GetAliasedFieldName(GetFieldName(rightSide), true));
            Target.SqlJoinClauses.Add(sb.ToString());
        }

        protected string GetParameter<TValue>(TValue value)
        {
            Target.SqlParameterCount++;
            var parameterName = string.Format(ParamaterFormat, Target.SqlParameterCount);
            Target.Parameters.Add(new KeyValuePair<string, object>(parameterName, Sqlize(value)));
            return parameterName;
        }

        protected string GetSqlListFrom<TValues>(IEnumerable<TValues> values)
        {
            values = values.ToList();

            if (!values.ToArray().Any())
                throw new Exception(
                    "Sequence contains no elements in your call to WhereIn/WhereNotIn, this would result in a global operation.  If that was your intention do not call WhereIn/WhereNotIn, otherwise verify that the collection you passed in actually contains values."
                );
            var sb = new StringBuilder();

            foreach (var value in values.ToArray())
            {
                Target.SqlParameterCount++;
                var parameterName = string.Format(ParamaterFormat, Target.SqlParameterCount);
                Target.Parameters.Add(new KeyValuePair<string, object>(parameterName, Sqlize(value)));
                sb.Append(parameterName);
                sb.Append(",");
            }
            var clause = sb.ToString();
            clause = clause.TrimEnd(',');
            return clause;
        }

        public static object Sqlize(object value)
        {
            if (value == null)
                return System.DBNull.Value;

            var type = value.GetType();
            //TODO: dates nulls?
            return type.IsEnum ? value.To<int>().To<string>() : value;
        }

        /// <summary>
        ///     For Debug Use Only
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        protected string SqlFormat<T>(T value)
        {
            if (value == null)
                return " NULL ";

            var outDate = DateTime.MinValue;
            bool isDate = DateTime.TryParse(value.ToString(), out outDate);
            var type = typeof(T);
            var format = "{0}";
            if (type == typeof(string) || isDate)
                format = "'{0}'";
            object valueToAppend = null;

            if (value == null)
                return " NULL ";

            //TODO: dates ?
            if (type.IsEnum)
                valueToAppend = value.To<int>();
            else
                valueToAppend = value;

            return string.Format(format, valueToAppend);
        }

        public string ToDebugString()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("SELECT * ");
            var last = Target.SetStatements.Any() ? Target.SetStatements.Last() : null;

            sb.AppendFormat(" FROM [{0}].[{1}] {2}", SqlSchema, SqlTableName, Prefix);
            if (Target.SqlJoinClauses.Any())
            {
                foreach (var join in Target.SqlJoinClauses)
                    sb.AppendLine(join);
            }
            var whereClause = GetWhereClause();
            var parameters = Parameters;
            parameters.Reverse();
            foreach (var param in parameters)
            {
                var value = SqlFormat(param.Value);
                whereClause = whereClause.Replace(param.Key, value);
            }
            sb.Append(whereClause);
            return sb.ToString();
        }
    }
}