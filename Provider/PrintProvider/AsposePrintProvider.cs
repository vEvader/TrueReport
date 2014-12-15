using System;
using System.Collections.Generic;
using System.Xml;
using Aspose.Pdf.Generator;
using Common.Entities;
using ProviderInterface;

namespace Provider.PrintProvider
{
    public class AsposePrintProvider : IPrintProvider
    {
        public byte[] PrintReport(List<ElementDto> template, XmlDocument dataSource)
        {
            Pdf pdfDocument = GetConfiguredPdf();

            int pageWidth = Convert.ToInt32(pdfDocument.PageSetup.PageWidth);
            int pageHeight = Convert.ToInt32(pdfDocument.PageSetup.PageHeight);

            Section section = pdfDocument.Sections.Add();
            Graph graph = new Graph(section, pageWidth, pageHeight - 1);
            section.Paragraphs.Add(graph);


            foreach (var element in template)
            {
                string bindedText = GetBindedText(element.BindValue, dataSource);

                switch (element.Type)
                {
                    case ElementType.Label:
                        FloatingBox labelText = GetFloatingBox(element, bindedText);
                        section.Paragraphs.Add(labelText);
                        break;

                    case ElementType.Edit:
                        FloatingBox editText = GetFloatingBox(element, bindedText);
                        section.Paragraphs.Add(editText);
                        Rectangle editBorder = GetRectangle(element, pageHeight);
                        graph.Shapes.Add(editBorder);
                        break;
                }

                //FloatingBox box1 = new FloatingBox(element.Width, element.Height);

                //box1.BoxHorizontalPositioning = BoxHorizontalPositioningType.Margin;
                //box1.BoxVerticalPositioning = BoxVerticalPositioningType.Page;
                //box1.Top = element.Y;
                //box1.Left = element.X;
                //section.Paragraphs.Add(box1);
                //string text = "";

                //box1.Paragraphs.Add(new Text(text));

            }
            return pdfDocument.GetBuffer();
        }

        private Rectangle GetRectangle(ElementDto element, float pageHeight)
        {
            return new Rectangle(element.X, pageHeight - element.Y, element.Width, -element.Height);
        }

        private FloatingBox GetFloatingBox(ElementDto element, string bindedText)
        {
            FloatingBox floatingBox = new FloatingBox(element.Width, element.Height);

            floatingBox.BoxHorizontalPositioning = BoxHorizontalPositioningType.Margin;
            floatingBox.BoxVerticalPositioning = BoxVerticalPositioningType.Page;
            floatingBox.Top = element.Y;
            floatingBox.Left = element.X;
            floatingBox.Paragraphs.Add(new Text(bindedText));

            return floatingBox;
        }

        private string GetBindedText(string bindValue, XmlDocument dataSource)
        {
            string result = string.Empty;
            try
            {
                var data = dataSource.SelectNodes("/report/" + bindValue);
                result = data.Count == 1 ? data[0].InnerText : "";
            }
            catch (Exception e)
            { }
            return result;
        }

        private Pdf GetConfiguredPdf()
        {
            Pdf pdfDocument = new Pdf();
            pdfDocument.PageSetup.PageBorder = new BorderInfo(0);
            pdfDocument.PageSetup.PageBorderMargin.Bottom = 0;
            pdfDocument.PageSetup.PageBorderMargin.Inner = 0;
            pdfDocument.PageSetup.PageBorderMargin.Left = 0;
            pdfDocument.PageSetup.PageBorderMargin.Outer = 0;
            pdfDocument.PageSetup.PageBorderMargin.Right = 0;
            pdfDocument.PageSetup.PageBorderMargin.Top = 0;
            pdfDocument.PageSetup.Margin.Bottom = 0;
            pdfDocument.PageSetup.Margin.Inner = 0;
            pdfDocument.PageSetup.Margin.Left = 0;
            pdfDocument.PageSetup.Margin.Outer = 0;
            pdfDocument.PageSetup.Margin.Right = 0;
            pdfDocument.PageSetup.Margin.Top = 0;
            return pdfDocument;
        }
    }
}
