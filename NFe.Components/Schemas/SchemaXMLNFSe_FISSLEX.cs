﻿using NFe.Components;

namespace NFSe.Components
{
    public class SchemaXMLNFSe_FISSLEX
    {
        public static void CriarListaIDXML()
        {
            #region XML de Consulta de Lote RPS

            SchemaXML.InfSchemas.Add("NFSE-FISSLEX-ConsultarLoteRpsEnvio", new InfSchema()
            {
                Tag = "ConsultarLoteRpsEnvio",
                ID = SchemaXML.InfSchemas.Count + 1,
                ArquivoXSD = "NFSe\\FISSLEX\\nfse.xsd",
                Descricao = "XML de Consulta de Lote RPS",
                TagAssinatura = "",
                TagAtributoId = "",
                TargetNameSpace = "http://www.abrasf.org.br/ABRASF/arquivos/nfse.xsd"
            });

            #endregion XML de Consulta de Lote RPS

            #region XML de Cancelamento de NFS-e

            SchemaXML.InfSchemas.Add("NFSE-FISSLEX-CancelarNfseEnvio", new InfSchema()
            {
                Tag = "CancelarNfseEnvio",
                ID = SchemaXML.InfSchemas.Count + 1,
                ArquivoXSD = "NFSe\\FISSLEX\\nfse.xsd",
                Descricao = "XML de Cancelamento da NFS-e",
                TagAssinatura = "",
                TagAtributoId = "",
                TargetNameSpace = "http://www.abrasf.org.br/ABRASF/arquivos/nfse.xsd"
            });

            #endregion XML de Cancelamento de NFS-e

            #region XML de Consulta Situação do Lote RPS

            SchemaXML.InfSchemas.Add("NFSE-FISSLEX-ConsultarSituacaoLoteRpsEnvio", new InfSchema()
            {
                Tag = "ConsultarSituacaoLoteRpsEnvio",
                ID = SchemaXML.InfSchemas.Count + 1,
                ArquivoXSD = "NFSe\\FISSLEX\\nfse.xsd",
                Descricao = "XML de Consulta da Situacao do Lote RPS",
                TagAssinatura = "",
                TargetNameSpace = "http://www.abrasf.org.br/ABRASF/arquivos/nfse.xsd"
            });

            #endregion XML de Consulta Situação do Lote RPS

            #region Consulta NFSe por Rps

            SchemaXML.InfSchemas.Add("NFSE-FISSLEX-ConsultarNfseRpsEnvio", new InfSchema()
            {
                Tag = "ConsultarNfseRpsEnvio",
                ID = SchemaXML.InfSchemas.Count + 1,
                ArquivoXSD = "NFSe\\FISSLEX\\nfse.xsd",
                Descricao = "XML de Consulta da NFSe por RPS",
                TagAssinatura = "",
                TagAtributoId = "",
                TargetNameSpace = "http://www.abrasf.org.br/ABRASF/arquivos/nfse.xsd"
            });

            #endregion Consulta NFSe por Rps

            #region Consulta NFSe por período

            SchemaXML.InfSchemas.Add("NFSE-FISSLEX-ConsultarNfseEnvio", new InfSchema()
            {
                Tag = "ConsultarNfseEnvio",
                ID = SchemaXML.InfSchemas.Count + 1,
                ArquivoXSD = "NFSe\\FISSLEX\\nfse.xsd",
                Descricao = "XML de Consulta da NFSe por período",
                TagAssinatura = "",
                TagAtributoId = "",
                TargetNameSpace = "http://www.abrasf.org.br/ABRASF/arquivos/nfse.xsd"
            });

            #endregion Consulta NFSe por período

            #region XML de Lote RPS

            SchemaXML.InfSchemas.Add("NFSE-FISSLEX-EnviarLoteRpsEnvio", new InfSchema()
            {
                Tag = "EnviarLoteRpsEnvio",
                ID = SchemaXML.InfSchemas.Count + 1,
                ArquivoXSD = "NFSe\\FISSLEX\\nfse.xsd",
                Descricao = "XML de Lote RPS",
                TagAssinatura = "Rps",
                TagAtributoId = "InfRps",
                TagLoteAssinatura = "EnviarLoteRpsEnvio",
                TagLoteAtributoId = "LoteRps",
                TargetNameSpace = "http://www.abrasf.org.br/ABRASF/arquivos/nfse.xsd"
            });

            #endregion XML de Lote RPS
        }
    }
}