﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using NFe.Components.Abstract;
using System.Net;
using System.Web.Services.Protocols;
using System.Security.Cryptography.X509Certificates;
using NFe.Components.br.gov.pr.guarapuava.nfse.p;

namespace NFe.Components.Pronin.GuarapuavaPR.p
{
    public class ProninP : EmiteNFSeBase
    {
        BasicHttpBinding_INFSEGeracao ServiceGeracao = new BasicHttpBinding_INFSEGeracao();
        BasicHttpBinding_INFSEConsultas ServiceConsultas = new BasicHttpBinding_INFSEConsultas();

        public override string NameSpaces
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        #region construtores
        public ProninP(TipoAmbiente tpAmb, string pastaRetorno, string usuarioProxy, string senhaProxy, string domainProxy, X509Certificate certificado)
            : base(tpAmb, pastaRetorno)
        {
            ServiceGeracao.Proxy = WebRequest.DefaultWebProxy;
            ServiceGeracao.Proxy.Credentials = new NetworkCredential(usuarioProxy, senhaProxy);
            ServiceGeracao.Credentials = new NetworkCredential(senhaProxy, senhaProxy);
            ServiceGeracao.ClientCertificates.Add(certificado);

            ServiceConsultas.Proxy = WebRequest.DefaultWebProxy;
            ServiceConsultas.Proxy.Credentials = new NetworkCredential(usuarioProxy, senhaProxy);
            ServiceConsultas.Credentials = new NetworkCredential(senhaProxy, senhaProxy);
            ServiceConsultas.ClientCertificates.Add(certificado);

            Cabecalho cabecMsg = new Cabecalho();
            cabecMsg.versao = "2.02";
            cabecMsg.versaoDados = "2.02";

            ServiceGeracao.cabecalho = cabecMsg;
            ServiceConsultas.cabecalho = cabecMsg;
        }

        /// <summary>
        /// Identificamos falha no certificado o do servidor, então temos que ignorar os erros
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="certificate"></param>
        /// <param name="chain"></param>
        /// <param name="sslPolicyErrors"></param>
        /// <returns></returns>
        private bool MyCertHandler(object sender, X509Certificate certificate, X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
        #endregion

        #region Métodos
        public override void EmiteNF(string file)
        {
            ServicePointManager.ServerCertificateValidationCallback = MyCertHandler;

            XmlDocument doc = new XmlDocument();
            doc.Load(file);

            string result = ServiceGeracao.RecepcionarLoteRps(doc.InnerXml);

            GerarRetorno(file, result, Propriedade.Extensao(Propriedade.TipoEnvio.EnvLoteRps).EnvioXML,
                                        Propriedade.Extensao(Propriedade.TipoEnvio.EnvLoteRps).RetornoXML);
        }

        public override void CancelarNfse(string file)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(file);
            string result = ServiceGeracao.CancelarNfse(doc.InnerXml);
            GerarRetorno(file, result, Propriedade.Extensao(Propriedade.TipoEnvio.PedCanNFSe).EnvioXML,
                                        Propriedade.Extensao(Propriedade.TipoEnvio.PedCanNFSe).RetornoXML);
        }


        public override void ConsultarLoteRps(string file)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(file);
            string result = ServiceConsultas.ConsultarLoteRps(doc.InnerXml);
            GerarRetorno(file, result, Propriedade.Extensao(Propriedade.TipoEnvio.PedLoteRps).EnvioXML,
                                        Propriedade.Extensao(Propriedade.TipoEnvio.PedLoteRps).RetornoXML);
        }

        public override void ConsultarSituacaoLoteRps(string file)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(file);
            string result = ServiceConsultas.ConsultarSituacaoLoteRps(doc.InnerXml);
            GerarRetorno(file, result, Propriedade.Extensao(Propriedade.TipoEnvio.PedSitLoteRps).EnvioXML,
                                        Propriedade.Extensao(Propriedade.TipoEnvio.PedSitLoteRps).RetornoXML);
        }

        public override void ConsultarNfse(string file)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(file);
            string result = ServiceConsultas.ConsultarNfse(doc.InnerXml);
            GerarRetorno(file, result, Propriedade.Extensao(Propriedade.TipoEnvio.PedSitNFSe).EnvioXML,
                                        Propriedade.Extensao(Propriedade.TipoEnvio.PedSitNFSe).RetornoXML);
        }

        public override void ConsultarNfsePorRps(string file)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(file);
            string result = ServiceConsultas.ConsultarNfsePorRps(doc.InnerXml);
            GerarRetorno(file, result, Propriedade.Extensao(Propriedade.TipoEnvio.PedSitNFSeRps).EnvioXML,
                                        Propriedade.Extensao(Propriedade.TipoEnvio.PedSitNFSeRps).RetornoXML);
        }

        #endregion
    }
}
