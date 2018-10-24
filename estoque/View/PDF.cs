using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View
{
    class PDF
    {
        public Document novo_arquivo(string dados)
        {
            ConsultaViabilidade aux = new ConsultaViabilidade();
            Document doc = new Document(PageSize.A4);
            doc.SetMargins(40, 40, 40, 80);
            doc.AddCreationDate();
            string caminho = @"C:\Users\anaca\Desktop\" + "RELATORIO DE COMPRAS _ " + dados + ".pdf"; ;
            
            var writer = PdfWriter.GetInstance(doc, new FileStream(caminho, FileMode.Create));

            return doc;
        }
        public void escreve(Document doc, string dados)
        {
           
            Paragraph paragrafo = new Paragraph("             LISTA DE COMPRAS", new Font(Font.NORMAL, 20));
            paragrafo.Add(dados);
            paragrafo.Alignment = Element.ALIGN_JUSTIFIED;
            
            doc.Add(paragrafo);
           
        }
    }
}
