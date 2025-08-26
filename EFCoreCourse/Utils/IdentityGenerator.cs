namespace EFCoreCourse.Utils
{
    public static class IdentityGenerator
    {
        public static Guid GenerateNewIdentity()
        {
            var randomBytes = Guid.NewGuid().ToByteArray();
            var timestamp = BitConverter.GetBytes(DateTime.UtcNow.Ticks);

            randomBytes[10] = timestamp[2];
            randomBytes[11] = timestamp[3];
            randomBytes[12] = timestamp[4];
            randomBytes[13] = timestamp[5];
            randomBytes[14] = timestamp[6];
            randomBytes[15] = timestamp[7];

            return new Guid(randomBytes);
        }
    }
}
