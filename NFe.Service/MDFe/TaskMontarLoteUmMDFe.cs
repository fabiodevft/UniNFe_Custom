﻿using NFe.Components;
using NFe.Settings;
using System;
using System.IO;
using System.Xml;

namespace NFe.Service
{
    /// <summary>
    /// Executar as tarefas pertinentes a assinatura e montagem do lote de uma única nota fiscal eletrônica
    /// </summary>
    public class TaskMDFeMontarLoteUm : TaskAbst
    {
        public TaskMDFeMontarLoteUm(string arquivo)
        {
            Servico = Servicos.MDFeMontarLoteUm;
            NomeArquivoXML = arquivo;
            ConteudoXML.PreserveWhitespace = false;
            ConteudoXML.Load(arquivo);
        }

        public override void Execute()
        {
            try
            {
                int emp = Empresas.FindEmpresaByThread();
                AssinarValidarXMLNFe(ConteudoXML);

                //Montar lote de nfe
                FluxoNfe oFluxoNfe = new FluxoNfe();

                string cError = "";
                try
                {
                    DadosNFeClass oDadosNfe = LerXMLNFe(ConteudoXML);
                    if (!oFluxoNfe.NFeComLote(oDadosNfe.chavenfe))
                    {
                        XmlDocument xmlLote = LoteNfe(ConteudoXML, NomeArquivoXML, oDadosNfe.versao);
                        TaskMDFeRecepcao mdfeRecepcao = new TaskMDFeRecepcao(xmlLote);
                        mdfeRecepcao.Execute();
                    }
                }
                catch (IOException ex)
                {
                    cError = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                }
                catch (Exception ex)
                {
                    cError = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                }

                if (!string.IsNullOrEmpty(cError))
                {
                    try
                    {
                        // grava o arquivo de erro
                        oAux.GravarArqErroERP(Path.GetFileNameWithoutExtension(NomeArquivoXML) + ".err", cError);
                        // move o arquivo para a pasta de erro
                        oAux.MoveArqErro(NomeArquivoXML);
                    }
                    catch
                    {
                        // A principio não vou fazer nada Wandrey 24/04/2011
                    }
                }
            }
            catch { }
        }
    }
}