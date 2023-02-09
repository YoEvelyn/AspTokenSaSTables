using Azure;
using Azure.Data.Tables;
using Azure.Data.Tables.Sas;

namespace AspTokenSaSTables.Services
{
    public class ServiceSaSToken
    {
        //Necesitamos el cliente de la tabla para generar el token
        private TableClient tableClient;
        public ServiceSaSToken(string azurekeys)
        {
            TableServiceClient serviceClient = new TableServiceClient(azurekeys);
            this.tableClient = serviceClient.GetTableClient("alumnos");
        }

        //Metodo que genera un token devolviendo unicamente permisos sobre un curso
        public string GenerateSasToken(string curso)
        {
            //Indicar los permisos que dejaremos sobre el acceso con el token
            TableSasPermissions permisos = TableSasPermissions.Read;
            //Indicar el tiempo de duración de los permisos
            TableSasBuilder builder = this.tableClient.GetSasBuilder(permisos, DateTime.Now.AddDays(1));
            //Al tratarse de tablas, solo vamos a permitir acceso a CURSO
            builder.PartitionKeyStart = curso;
            builder.PartitionKeyEnd = curso;
            //Con esto se genera una Url (uri) con los permisos del acceso y el token
            Uri uriToken = this.tableClient.GenerateSasUri(builder);
            string token = uriToken.AbsoluteUri;
            return token;
        }
    }
}
