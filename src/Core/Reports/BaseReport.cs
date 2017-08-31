using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Voodoo;
using Voodoo.Reports;
using Voodoo.Reports.Models;

namespace Core.Reports
{
    public class BaseReport : Report
    {
        protected Table table;
        private string title;

        public BaseReport(string title)
        {
            this.title = title;
            this.Body.Border(BorderPosition.Top, BorderPosition.Bottom);
            addHeader();
            AddDefaultFooter();
            //ShowRuler = true;
        }

        public byte[] GetImage(string imageName)
        {
            imageName = $"Core.Reports.Tests.Images.{imageName}";
            var assembly = this.GetType().Assembly;
            using (var resFilestream = assembly.GetManifestResourceStream(imageName))
            {
                if (resFilestream == null) return null;
                byte[] bytes = new byte[resFilestream.Length];
                resFilestream.Read(bytes, 0, bytes.Length);
                return bytes;
            }
        }

        private void addHeader()
        {
            var header = Header.AddTable().Italics().ForeColor(System.Drawing.Color.Blue);
            header.NoBorder();
            header.AddColumn(1.5);
            header.AddColumn(4);
            header.AddColumn(1.5);

            var image = GetImage("logo.png");
            var row = header.AddRow();
            var left = row.AddCell().AddImage(image);
            var middle = row.AddCell().Bold().Big().Big().Big()
                .Center().AddFragment(title);
            var right = row.AddCell();
        }
    }
}