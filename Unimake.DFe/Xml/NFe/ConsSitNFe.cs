﻿using System;
using System.Xml;

namespace Unimake.DFe.Xml.NFe
{
    public class ConsSitNFe : BaseXml
    {
        public string versao { get; set; }
        public string xmlns { get; set; }
        public int tpAmb { get; set; }
        public string xServ { get; set; }
        public int tpEmis { get; set; }
        public string chNFe { get; set; }

        /// <summary>
        /// Efetua a leitura do XML e disponibilizar o conteúdo nas propriedades da classe
        /// </summary>
        /// <param name="doc">XML a ser lido</param>
        public override void Ler(XmlDocument doc)
        {
            XmlNodeList xmlNodeList = doc.GetElementsByTagName("consSitNFe");
            XmlElement xmlElement = (XmlElement)xmlNodeList[0];

            versao = xmlElement.GetAttribute("versao");
            if (xmlElement.GetAttribute("xmlns") != null)
            {
                xmlns = xmlElement.GetAttribute("xmlns");
            }
            tpAmb = Convert.ToInt32(xmlElement.GetElementsByTagName("tpAmb")[0].InnerText);
            chNFe = xmlElement.GetElementsByTagName("chNFe")[0].InnerText;
            xServ = xmlElement.GetElementsByTagName("xServ")[0].InnerText;
            if (xmlElement.GetElementsByTagName("tpEmis")[0] != null)
            {
                tpEmis = Convert.ToInt32(xmlElement.GetElementsByTagName("tpEmis")[0].InnerText);
            }
            else
            {
                tpEmis = 1;
            }
        }

        public override XmlDocument Gerar()
        {
            throw new NotImplementedException();
        }
    }
}