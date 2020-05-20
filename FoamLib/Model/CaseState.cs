using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoamLib.Model
{
    [Serializable]
    [JsonObject(MemberSerialization.OptOut)]
    public class CaseState
    {
        bool isMeshPrepared = false;
        bool isCaseInitlized = false;
        bool isCaseSolved = false;

        public CaseState()
        {
        }

        public CaseState(bool isMeshPrepared, bool isCaseInitlized, bool isCaseSolved)
        {
            this.isMeshPrepared = isMeshPrepared;
            this.isCaseInitlized = isCaseInitlized;
            this.isCaseSolved = isCaseSolved;
        }

        public bool IsMeshPrepared { get => isMeshPrepared; set => isMeshPrepared = value; }
        public bool IsCaseInitlized { get => isCaseInitlized; set => isCaseInitlized = value; }
        public bool IsCaseSolved { get => isCaseSolved; set => isCaseSolved = value; }
    }
}
