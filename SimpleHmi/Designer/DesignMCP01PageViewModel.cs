using SimpleHmi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHmi.Designer
{
    class DesignMCP01PageViewModel : MCP01ViewModel
    {
        public DesignMCP01PageViewModel() : base(new DesignPlcService()) 
        { 
        }
    }
}