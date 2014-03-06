using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Code.Controller;

namespace Assets.Code.Editor.Stubs
{
    /// <summary>
    /// Any Unit under test that uses the server should inject this stub
    /// </summary>
    class ServerStub : Server
    {
        public ServerStub()
            : base(null)
            { }
        //TODO: Override interface for Unit Testing
    }
}
