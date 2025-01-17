﻿using System.Xml;
using Unimake.Business.DFe.ConfigurationManager;

namespace Unimake.Business.DFe.Servicos
{
    public abstract class ServicoBase
    {
        #region Private Methods

        /// <summary>
        /// Definir o nome da tag que contem as propriedades de acordo com o serviço que está sendo executado
        /// </summary>
        /// <returns>Nome da tag</returns>
        private string DefinirNomeTag() => GetType().Name;

        /// <summary>
        /// Efetua a leitura do XML que contem configurações específicas de cada webservice e atribui o conteúdo nas propriedades do objeto "Configuracoes"
        /// </summary>
        private void LerXmlConfigEspecifico(string xmlConfigEspecifico)
        {
            var doc = new XmlDocument();
            doc.Load(xmlConfigEspecifico);

            var listServicos = doc.GetElementsByTagName("Servicos");
            foreach(var nodeServicos in listServicos)
            {
                var elementServicos = (XmlElement)nodeServicos;

                if(elementServicos.GetAttribute("ID") == Configuracoes.TipoDFe.ToString())
                {
                    var nomeTagServico = DefinirNomeTag();

                    var listPropriedades = elementServicos.GetElementsByTagName(nomeTagServico);

                    foreach(var nodePropridades in listPropriedades)
                    {
                        var elementPropriedades = (XmlElement)nodePropridades;
                        if(elementPropriedades.GetAttribute("versao") == Configuracoes.SchemaVersao)
                        {
                            Configuracoes.Descricao = elementPropriedades.GetElementsByTagName("Descricao")[0].InnerText;
                            Configuracoes.WebActionHomologacao = elementPropriedades.GetElementsByTagName("WebActionHomologacao")[0].InnerText;
                            Configuracoes.WebActionProducao = elementPropriedades.GetElementsByTagName("WebActionProducao")[0].InnerText;
                            Configuracoes.WebEnderecoHomologacao = elementPropriedades.GetElementsByTagName("WebEnderecoHomologacao")[0].InnerText;
                            Configuracoes.WebEnderecoProducao = elementPropriedades.GetElementsByTagName("WebEnderecoProducao")[0].InnerText;
                            Configuracoes.WebContentType = elementPropriedades.GetElementsByTagName("WebContentType")[0].InnerText;
                            Configuracoes.WebSoapString = elementPropriedades.GetElementsByTagName("WebSoapString")[0].InnerText;
                            Configuracoes.WebSoapVersion = elementPropriedades.GetElementsByTagName("WebSoapVersion")[0].InnerText;
                            Configuracoes.WebTagRetorno = elementPropriedades.GetElementsByTagName("WebTagRetorno")[0].InnerText;
                            Configuracoes.TargetNS = elementPropriedades.GetElementsByTagName("TargetNS")[0].InnerText;
                            Configuracoes.SchemaVersao = elementPropriedades.GetElementsByTagName("SchemaVersao")[0].InnerText;
                            Configuracoes.SchemaArquivo = elementPropriedades.GetElementsByTagName("SchemaArquivo")[0].InnerText.Replace("{0}", Configuracoes.SchemaVersao);
                            Configuracoes.TagAssinatura = elementPropriedades.GetElementsByTagName("TagAssinatura")[0].InnerText;
                            Configuracoes.TagAtributoID = elementPropriedades.GetElementsByTagName("TagAtributoID")[0].InnerText;
                            Configuracoes.TagLoteAssinatura = elementPropriedades.GetElementsByTagName("TagLoteAssinatura")[0].InnerText;
                            Configuracoes.TagLoteAtributoID = elementPropriedades.GetElementsByTagName("TagLoteAtributoID")[0].InnerText;

                            //Verificar se existem schemas específicos de validação
                            if(elementPropriedades.GetElementsByTagName("SchemasEspecificos")[0] != null)
                            {
                                var listSchemasEspecificios = elementPropriedades.GetElementsByTagName("SchemasEspecificos");

                                foreach(var nodeSchemasEspecificos in listSchemasEspecificios)
                                {
                                    var elemenSchemasEspecificos = (XmlElement)nodeSchemasEspecificos;

                                    var listTipo = elemenSchemasEspecificos.GetElementsByTagName("Tipo");

                                    foreach(var nodeTipo in listTipo)
                                    {
                                        var elementTipo = (XmlElement)nodeTipo;
                                        var idSchemaEspecifico = elementTipo.GetElementsByTagName("ID")[0].InnerText;

                                        Configuracoes.SchemasEspecificos.Add(idSchemaEspecifico, new SchemaEspecifico
                                        {
                                            Id = idSchemaEspecifico,
                                            SchemaArquivo = elementTipo.GetElementsByTagName("SchemaArquivo")[0].InnerText.Replace("{0}", Configuracoes.SchemaVersao),
                                            SchemaArquivoEspecifico = elementTipo.GetElementsByTagName("SchemaArquivoEspecifico")[0].InnerText.Replace("{0}", Configuracoes.SchemaVersao)
                                        });
                                    }
                                }
                            }
                        }
                    }

                    break;
                }
            }
        }

        #endregion Private Methods

        #region Protected Fields

        protected Configuracao Configuracoes;

        protected XmlDocument ConteudoXML;

        #endregion Protected Fields

        #region Protected Methods

        /// <summary>
        /// Defini o valor das propriedades do objeto "Configuracoes"
        /// </summary>
        protected abstract void DefinirConfiguracao();

        /// <summary>
        /// Método para validar o schema do XML
        /// </summary>
        protected abstract void XmlValidar();

        #endregion Protected Methods

        #region Protected Internal Methods

        /// <summary>
        /// Inicializa configurações, parmâtros e propriedades para execução do serviço.
        /// </summary>
        protected internal void Inicializar()
        {
            if(!Configuracoes.Definida)
            {
                DefinirConfiguracao();
                LerXmlConfigGeral();
            }
        }

        /// <summary>
        /// Efetua a leitura do XML que contem configurações gerais e atribui o conteúdo nas propriedades do objeto "Configuracoes"
        /// </summary>
        protected internal void LerXmlConfigGeral()
        {
            var doc = new XmlDocument();
            doc.Load(CurrentConfig.ArquivoConfigGeral);

            var listConfiguracoes = doc.GetElementsByTagName("Configuracoes");

            foreach(XmlNode nodeConfiguracoes in listConfiguracoes)
            {
                var elementConfiguracoes = (XmlElement)nodeConfiguracoes;
                var listArquivos = elementConfiguracoes.GetElementsByTagName("Arquivo");

                foreach(var nodeArquivos in listArquivos)
                {
                    var elementArquivos = (XmlElement)nodeArquivos;

                    if(elementArquivos.GetAttribute("ID") == Configuracoes.CodigoUF.ToString())
                    {
                        Configuracoes.Nome = elementArquivos.GetElementsByTagName("Nome")[0].InnerText;
                        Configuracoes.NomeUF = elementArquivos.GetElementsByTagName("UF")[0].InnerText;

                        LerXmlConfigEspecifico(CurrentConfig.PastaArqConfig + elementArquivos.GetElementsByTagName("ArqConfig")[0].InnerText);

                        break;
                    }
                }
            }
        }

        #endregion Protected Internal Methods

        #region Public Fields

        public string RetornoWSString;
        public XmlDocument RetornoWSXML;

        #endregion Public Fields

        #region Public Constructors

        public ServicoBase(XmlDocument conteudoXML, Configuracao configuracao)
        {
            Configuracoes = configuracao;
            ConteudoXML = conteudoXML;

            Inicializar();

            System.Diagnostics.Trace.WriteLine(ConteudoXML?.InnerXml, "Unimake.DFe");
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        /// Executar o serviço para consumir o webservice
        /// </summary>
        public abstract void Executar();

        /// <summary>
        /// Gravar o XML de distribuição em uma pasta no HD
        /// </summary>
        /// <param name="pasta">Pasta onde deve ser gravado o XML no HD</param>
        /// <param name="nomeArquivo">Nome do arquivo a ser gravado no HD</param>
        /// <param name="conteudoXML">String contendo o conteúdo do XML a ser gravado no HD</param>
        public abstract void GravarXmlDistribuicao(string pasta, string nomeArquivo, string conteudoXML);

        #endregion Public Methods
    }
}