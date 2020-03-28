using System.Threading;

namespace VpNet.ManagedApi.System
{
    internal static class ObjectReferenceCounter
    {
        private static readonly ReaderWriterLockSlim _rwl = new ReaderWriterLockSlim();

        private static int _reference = int.MinValue;

        internal static int GetNextReference()
        {
            int ret;
            _rwl.EnterWriteLock();
            if (_reference < int.MaxValue)
                ret = _reference++;
            else
                ret = _reference = int.MinValue;
            _rwl.ExitWriteLock();
            return ret;
        }
    }
}
