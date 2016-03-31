namespace GSDRequirementsCSharp.Persistence.Migrations
{
    using MySql.Data.Entity;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<GSDRequirementsCSharp.Persistence.GSDRequirementsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;

            DbConfiguration.SetConfiguration(new MySqlEFConfiguration());
            SetSqlGenerator("MySql.Data.MySqlClient", new MySqlMigrationSqlGenerator());
            SetHistoryContextFactory(MySqlProviderInvariantName.ProviderName, 
                                    (connection, schema) => new MySqlHistoryContext(connection, schema));
        }

        protected override void Seed(GSDRequirementsContext context)
        {
        }
    }
}
