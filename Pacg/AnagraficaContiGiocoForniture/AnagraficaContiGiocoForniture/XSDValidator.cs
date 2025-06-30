using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Schema;
using System.Xml;

namespace AnagraficaContiGiocoForniture
{
    public class XSDValidator
    {
        static int ErrorsCount = 0;
        static string ErrorMessage = "";




        public static void ValidationHandler(object sender,
                                         ValidationEventArgs args)
        {
            ErrorMessage = ErrorMessage + args.Message + "\r\n";
            ErrorsCount++;
        }

        public void Validate(string strXMLDoc, string urlpath)
        {
            try
            {
                // Declare local objects
                XmlTextReader tr = null;
                XmlSchemaCollection xsc = null;
                XmlValidatingReader vr = null;

                // Text reader object
                tr = new XmlTextReader(urlpath);
                xsc = new XmlSchemaCollection();
                xsc.Add(null, tr);

                // XML validator object

                vr = new XmlValidatingReader(strXMLDoc,
                             XmlNodeType.Document, null);

                vr.Schemas.Add(xsc);

                // Add validation event handler

                vr.ValidationType = ValidationType.Schema;
                vr.ValidationEventHandler +=
                         new ValidationEventHandler(ValidationHandler);

                // Validate XML data

                while (vr.Read()) ;

                vr.Close();

                // Raise exception, if XML validation fails
                if (ErrorsCount > 0)
                {
                    throw new Exception(ErrorMessage);
                }

                // XML Validation succeeded
                Console.WriteLine("XML validation succeeded.\r\n");
            }
            catch (Exception error)
            {
                // XML Validation failed
                Console.WriteLine("XML validation failed." + "\r\n" +
                "Error Message: " + error.Message);
            }
        }
    }
}


    