using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VidlyDB;

namespace VidlyBL.DataAccess
{
    public class VidlyEntitiesSingleton
    {
        #region constructor
        private static readonly Lazy<VidlyEntities> _instance =
            new Lazy<VidlyEntities>(() => new VidlyEntities());

        public static VidlyEntities Instance => _instance.Value;
        private VidlyEntitiesSingleton() { }

        ~VidlyEntitiesSingleton() => _instance?.Value.Dispose();
        #endregion
    }
}
