//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Core.Operations.Members.Extras;
//using Voodoo;
//using Voodoo.Reports;
//using Voodoo.Reports.Models;

//namespace Core.Reports
//{
//    public class MemberListReport:BaseReport
//    {
//        public MemberListReport() : base("Member List")
//        {
//        }
//        public void Build(List<MemberRow> data)
//        {
//            table = this.Body.AddTable();
//            table.AddColumn(1.8);
//            table.AddColumn(1.8);
//            table.AddColumn(1.8);            
//            addHeaderRow();
//            data.ForEach(addRow);
//        }

//        private void addHeaderRow()
//        {
//            var header = this.table.AddRow().Bold().Big().Header();
//            header.NoBorder(BorderPosition.Top);

//            header.AddCell("Name");
//            header.AddCell("Optional Int");
//            header.AddCell("Required Int");
//        }

//        private void addRow(MemberRow person)
//        {
//            var row = table.AddRow();

//            row.AddCell($"{person.Name}");
//            row.AddCell(person.OptionalInt.To<string>());
//            row.AddCell(person.RequiredInt.To<string>());

//        }
//    }
//}
