﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 15.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Voodoo.CodeGeneration.Templates.Web.ProjectLevel.WebFrameworks.Aurelia
{
    /// <summary>
    ///     Class to produce the template output
    /// </summary>
    [GeneratedCode("Microsoft.VisualStudio.TextTemplating", "15.0.0.0")]
    public partial class TsServiceBatchTemplate : TsServiceBatchTemplateBase
    {
        /// <summary>
        ///     Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            Write(@"//***************************************************************
//This code just called you a tool
//What I meant to say is that this code was generated by a tool
//so don't mess with it unless you're debugging
//subject to change without notice, might regenerate while you're reading, etc
//***************************************************************
import { HttpClient, HttpResponseMessage } from 'aurelia-http-client';
import { autoinject, inject } from ""aurelia-framework"";
import * as Models from ""./models.generated"";
import { MessengerService } from ""./services/messenger-service"";
import { EncoderService } from ""./services/encoder-service"";
import { AjaxService } from ""./services/ajax-service"";



");
            foreach (var resource in File.Resources)
            {
                Write("@autoinject()\r\n\texport class ");
                Write(ToStringHelper.ToStringWithCulture(resource.Name));
                Write("\r\n\t{\r\n\t\turl: string = \'api/");
                Write(ToStringHelper.ToStringWithCulture(resource.Name));
                Write("\';\r\n\r\n\t\tconstructor(private ajaxService: AjaxService, private messenger:Messenger" +
                      "Service)\r\n\t\t{\r\n\t\t}\r\n\r\n");

                foreach (var verb in resource.Verbs)
                {
                    var declarations = File.Builder.AddTypes(verb.RequestType, verb.ResponseType);
                    Write("\r\n\t\t\tpublic async ");
                    Write(ToStringHelper.ToStringWithCulture(verb.Name.ToLower()));
                    Write(" (request: Models.");
                    Write(ToStringHelper.ToStringWithCulture(declarations.RequestDeclaration));
                    Write("):\r\n\t\t\t\t\t\t\t\t\t\t\tPromise<Models.");
                    Write(ToStringHelper.ToStringWithCulture(declarations.ResponseDeclaration));
                    Write(
                        ">\r\n\t\t\t{\r\n\r\n\t\t\tthis.messenger.incrementHttpRequestCounter();\r\n        var requestB" +
                        "uilder = await this.ajaxService.build");
                    Write(ToStringHelper.ToStringWithCulture(verb.Name));
                    Write(@"Request(request, this.url);
        try {
            var response = await requestBuilder.send();
            this.messenger.decrementHttpRequestCounter();
            var out = <Models.IResponse>JSON.parse(response.response);
            this.messenger.showResponseMessage(out);
            return out;
        }
        catch (err)
        {
            
            this.ajaxService.logError(err, this.url, (<any>new Error()).stack);
           
            var result = {
                isOk: false,
                message: err.statusText
            };
           
            this.messenger.decrementHttpRequestCounter();
            this.messenger.showResponseMessage(result);
            return result;
        }
		}
	");
                }
                Write("\t}\r\n\t");
            }
            return GenerationEnvironment.ToString();
        }
    }

    #region Base class

    /// <summary>
    ///     Base class for this transformation
    /// </summary>
    [GeneratedCode("Microsoft.VisualStudio.TextTemplating", "15.0.0.0")]
    public class TsServiceBatchTemplateBase
    {
        #region Fields

        private StringBuilder generationEnvironmentField;
        private CompilerErrorCollection errorsField;
        private List<int> indentLengthsField;
        private bool endsWithNewline;

        #endregion

        #region Properties

        /// <summary>
        ///     The string builder that generation-time code is using to assemble generated output
        /// </summary>
        protected StringBuilder GenerationEnvironment
        {
            get
            {
                if (generationEnvironmentField == null)
                    generationEnvironmentField = new StringBuilder();
                return generationEnvironmentField;
            }
            set  =>
            generationEnvironmentField  =
            value;
        }

        /// <summary>
        ///     The error collection for the generation process
        /// </summary>
        public CompilerErrorCollection Errors
        {
            get
            {
                if (errorsField == null)
                    errorsField = new CompilerErrorCollection();
                return errorsField;
            }
        }

        /// <summary>
        ///     A list of the lengths of each indent that was added with PushIndent
        /// </summary>
        private List<int> indentLengths
        {
            get
            {
                if (indentLengthsField == null)
                    indentLengthsField = new List<int>();
                return indentLengthsField;
            }
        }

        /// <summary>
        ///     Gets the current indent we use when adding lines to the output
        /// </summary>
        public string CurrentIndent { get; private set; } = "";

        /// <summary>
        ///     Current transformation session
        /// </summary>
        public virtual IDictionary<string, object> Session { get; set; }

        #endregion

        #region Transform-time helpers

        /// <summary>
        ///     Write text directly into the generated output
        /// </summary>
        public void Write(string textToAppend)
        {
            if (string.IsNullOrEmpty(textToAppend))
                return;
            // If we're starting off, or if the previous text ended with a newline,
            // we have to append the current indent first.
            if (GenerationEnvironment.Length == 0
                || endsWithNewline)
            {
                GenerationEnvironment.Append(CurrentIndent);
                endsWithNewline = false;
            }
            // Check if the current text ends with a newline
            if (textToAppend.EndsWith(Environment.NewLine, StringComparison.CurrentCulture))
                endsWithNewline = true;
            // This is an optimization. If the current indent is "", then we don't have to do any
            // of the more complex stuff further down.
            if (CurrentIndent.Length == 0)
            {
                GenerationEnvironment.Append(textToAppend);
                return;
            }
            // Everywhere there is a newline in the text, add an indent after it
            textToAppend = textToAppend.Replace(Environment.NewLine, Environment.NewLine + CurrentIndent);
            // If the text ends with a newline, then we should strip off the indent added at the very end
            // because the appropriate indent will be added when the next time Write() is called
            if (endsWithNewline)
                GenerationEnvironment.Append(textToAppend, 0, textToAppend.Length - CurrentIndent.Length);
            else
                GenerationEnvironment.Append(textToAppend);
        }

        /// <summary>
        ///     Write text directly into the generated output
        /// </summary>
        public void WriteLine(string textToAppend)
        {
            Write(textToAppend);
            GenerationEnvironment.AppendLine();
            endsWithNewline = true;
        }

        /// <summary>
        ///     Write formatted text directly into the generated output
        /// </summary>
        public void Write(string format, params object[] args)
        {
            Write(string.Format(CultureInfo.CurrentCulture, format, args));
        }

        /// <summary>
        ///     Write formatted text directly into the generated output
        /// </summary>
        public void WriteLine(string format, params object[] args)
        {
            WriteLine(string.Format(CultureInfo.CurrentCulture, format, args));
        }

        /// <summary>
        ///     Raise an error
        /// </summary>
        public void Error(string message)
        {
            var error = new CompilerError();
            error.ErrorText = message;
            Errors.Add(error);
        }

        /// <summary>
        ///     Raise a warning
        /// </summary>
        public void Warning(string message)
        {
            var error = new CompilerError();
            error.ErrorText = message;
            error.IsWarning = true;
            Errors.Add(error);
        }

        /// <summary>
        ///     Increase the indent
        /// </summary>
        public void PushIndent(string indent)
        {
            if (indent == null)
                throw new ArgumentNullException("indent");
            CurrentIndent = CurrentIndent + indent;
            indentLengths.Add(indent.Length);
        }

        /// <summary>
        ///     Remove the last indent that was added with PushIndent
        /// </summary>
        public string PopIndent()
        {
            var returnValue = "";
            if (indentLengths.Count > 0)
            {
                var indentLength = indentLengths[indentLengths.Count - 1];
                indentLengths.RemoveAt(indentLengths.Count - 1);
                if (indentLength > 0)
                {
                    returnValue = CurrentIndent.Substring(CurrentIndent.Length - indentLength);
                    CurrentIndent = CurrentIndent.Remove(CurrentIndent.Length - indentLength);
                }
            }
            return returnValue;
        }

        /// <summary>
        ///     Remove any indentation
        /// </summary>
        public void ClearIndent()
        {
            indentLengths.Clear();
            CurrentIndent = "";
        }

        #endregion

        #region ToString Helpers

        /// <summary>
        ///     Utility class to produce culture-oriented representation of an object as a string.
        /// </summary>
        public class ToStringInstanceHelper
        {
            private IFormatProvider formatProviderField = CultureInfo.InvariantCulture;

            /// <summary>
            ///     Gets or sets format provider to be used by ToStringWithCulture method.
            /// </summary>
            public IFormatProvider FormatProvider
            {
                get  =>
                formatProviderField;
                set
                {
                    if (value != null)
                        formatProviderField = value;
                }
            }

            /// <summary>
            ///     This is called from the compile/run appdomain to convert objects within an expression block to a string
            /// </summary>
            public string ToStringWithCulture(object objectToConvert)
            {
                if (objectToConvert == null)
                    throw new ArgumentNullException("objectToConvert");
                var t = objectToConvert.GetType();
                var method = t.GetMethod("ToString", new[]
                {
                    typeof(IFormatProvider)
                });
                if (method == null)
                    return objectToConvert.ToString();
                return (string) method.Invoke(objectToConvert, new object[]
                {
                    formatProviderField
                });
            }
        }

        /// <summary>
        ///     Helper to produce culture-oriented representation of an object as a string
        /// </summary>
        public ToStringInstanceHelper ToStringHelper { get; } = new ToStringInstanceHelper();

        #endregion
    }

    #endregion
}