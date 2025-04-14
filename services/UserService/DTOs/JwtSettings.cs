namespace UserService.DTOs
{
    public class JwtSettings
    {
        public bool ValidateIssuerSignature { get; set; }
        public string? IssuerSigningKey { get; set; }
        public bool ValidateIssuer { get; set; }
        public string? ValidIssuer { get; set; }
        public bool ValidateAudience { get; set; }
        public string? ValidAudience { get; set; }
        public bool RequireExpirationTime { get; set; }
        public bool ValidateLifetime { get; set; }
    }
}
