namespace FODUN.Reservas.Configurations { 
    public class AdminSettings
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string NombreCompleto { get; set; }
        public string NumeroDocumento { get; set; }
        public string Celular { get; set; }
        public string TelefonoResidencia { get; set; }
        public string DireccionResidencia { get; set; }
        public string Barrio { get; set; }
        public string Municipio { get; set; }
        public string Departamento { get; set; }
        public string PreguntaSecreta { get; set; }
        public string RespuestaSecreta { get; set; }
        public bool AutorizaEnvioInfoCelular { get; set; }
        public bool AutorizaEnvioInfoCorreo { get; set; }
    }
}