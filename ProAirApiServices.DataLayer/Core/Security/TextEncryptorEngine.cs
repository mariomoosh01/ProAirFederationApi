using Konscious.Security.Cryptography;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace ProAirApiServices.DataLayer.Core.Security
{
    public sealed class TextEncryptorEngine
    {
        private readonly IConfiguration _config;
        private Argon2id _argonEngine;
        public TextEncryptorEngine(IConfiguration config)
        {
            _config = config;
            _argonEngine = default!;
        }

        public byte[] EncryptPassword(string password) 
        {
            _argonEngine = new Argon2id(Encoding.UTF8.GetBytes(password));

            ArgonSetup();

            return _argonEngine.GetBytes(32);
        }

        public bool VerifyHash(string password, byte[] hash)
        {
            var newHash = EncryptPassword(password);
            return hash.SequenceEqual(newHash);
        }

        private void ArgonSetup()
        {
            if (_argonEngine == null) return;

            _argonEngine.DegreeOfParallelism = Convert.ToInt16(_config["HashConfigs:parallelism"]);
            _argonEngine.Iterations = Convert.ToInt16(_config["HashConfigs:iterations"]);
            _argonEngine.MemorySize = Convert.ToInt16(_config["HashConfigs:memory"]);
            _argonEngine.Salt = CreateSalt();
        }

        private byte[] CreateSalt()
        {
            var buffer = new byte[16];
            var saltStr = RandomNumberGenerator.Create(_config["HashConfigs:salt"]??"this is a super random string");
            
            if(saltStr != null)
                saltStr.GetBytes(buffer);
            
            return buffer;
        }
    }
}
