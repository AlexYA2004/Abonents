namespace Tests
{
    public class DbConnectionTests
    {
        private IDbConnection _connection;

        [SetUp]
        public void Setup()
        {
            _connection = new SqlConnection("Data Source=AlexYA2004\\SQLEXPRESS;Initial Catalog=AbonentsInfo;Integrated Security=True;Connect Timeout=30;Encrypt=False;");
        }

        [Test]
        public void TestDatabaseConnection()
        {  
            _connection.Open();
            
            Assert.AreEqual(ConnectionState.Open, _connection.State, "Не удалось подключиться к базе данных. Проверьте подключение.");

            _connection.Close();
        }

        [TearDown]
        public void Cleanup()
        {
            _connection.Dispose();
        }
    }
}