using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;
using SchoolManagement.Domain.Interfaces.Security.PasswordHashing;

namespace SchoolManagement.Infrastructure.Security.PasswordHashing
{
	internal sealed class Argon2PasswordHasher : IPasswordHasher
	{

		private const int DEGREE_OF_PARALLELISM = 1;
		private const int ITERATIONS = 2;
		private const int MEMORY_SIZE = 20 * 1024;
		private const int SALT_SIZE = 16;
		private const int HASH_SIZE = 32;

		public string HashPassword(string password)
		{
			byte[] salt = RandomNumberGenerator.GetBytes(SALT_SIZE);


			byte[] hashPasswordBytes = HashPassword(password, salt);


			byte[] combinedBytes = new byte[hashPasswordBytes.Length + salt.Length];

			salt.CopyTo(combinedBytes);
			hashPasswordBytes.CopyTo(combinedBytes, index: salt.Length);

			string stringHash = Convert.ToBase64String(combinedBytes);

			return stringHash;
		}

		public bool VerifyPassword(string password, string passwordHash)
		{
			byte[] combinedBytes = Convert.FromBase64String(passwordHash);

			byte[] salt = new byte[SALT_SIZE];
			byte[] hashBytes = new byte[HASH_SIZE];

			Array.Copy(combinedBytes, salt, SALT_SIZE);
			Array.Copy(combinedBytes, SALT_SIZE, hashBytes, 0, HASH_SIZE);

			byte[] hashPasswordBytes = HashPassword(password, salt);

			bool isHashesEqual = CryptographicOperations.FixedTimeEquals(hashBytes, hashPasswordBytes);

			return isHashesEqual;

		}


		private byte[] HashPassword(string password, byte[] salt)
		{
			byte[] passwordBytes = Encoding.UTF8.GetBytes(password);


			Argon2id hashAlgorithm = new(passwordBytes)
			{
				DegreeOfParallelism = DEGREE_OF_PARALLELISM,
				Iterations = ITERATIONS,
				MemorySize = MEMORY_SIZE,
				Salt = salt
			};

			byte[] hashBytes = hashAlgorithm.GetBytes(HASH_SIZE);

			return hashBytes;
		}
	}
}
