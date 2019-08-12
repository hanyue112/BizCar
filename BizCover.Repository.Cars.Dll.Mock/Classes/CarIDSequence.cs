namespace BizCover.Repository.Cars.Dll.Mock.Classes
{
    public static class CarIDSequence
    {
        private readonly static object lck = new object();
        private static int _id = 0;

        public static int AcquireID()
        {
            lock (lck)
            {
                return ++_id;
            }
        }
    }
}