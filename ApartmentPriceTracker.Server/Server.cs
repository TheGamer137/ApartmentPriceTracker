namespace ApartmentPriceTracker.SecondTask
{
    public static class Server
    {
        private static int count = 0;
        private static readonly object countLock = new object();

        public static int GetCount()
        {
            // Читатели могут читать параллельно
            return count;
        }

        public static void AddToCount(int value)
        {
            lock (countLock)
            {
                // Писатели пишут только последовательно
                // Ждем, если уже есть другие операции записи
                // (читатели могут продолжать читать)
                Monitor.Enter(countLock);

                try
                {
                    // Выполняем операцию записи
                    count += value;

                    // Сообщаем о завершении операции записи
                    Monitor.PulseAll(countLock);
                }
                finally
                {
                    // Выходим из блокировки
                    Monitor.Exit(countLock);
                }
            }
        }
    }
}
