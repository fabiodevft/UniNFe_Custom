﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace NFe.Components.VersaTecnologia
{
    public class VersaTecnologia : VersaTecnologiaBase
    {
        public override string NameSpaces
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        #region Construtures

        public VersaTecnologia(TipoAmbiente tpAmb, string pastaRetorno, int codMun, string usuarioProxy, string senhaProxy, string domainProxy, X509Certificate certificado)
            : base(tpAmb, pastaRetorno, codMun, usuarioProxy, senhaProxy, domainProxy, certificado)
        { }

        #endregion

    }
}
