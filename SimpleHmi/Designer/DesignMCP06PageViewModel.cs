using SimpleHmi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHmi.Designer
{
    class DesignMCP06PageViewModel : MCP06ViewModel
    {
        public DesignMCP06PageViewModel() : base(new DesignPlcService())
        {

        }
    }
}
