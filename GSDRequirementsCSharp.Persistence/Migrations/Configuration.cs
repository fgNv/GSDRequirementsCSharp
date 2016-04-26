namespace GSDRequirementsCSharp.Persistence.Migrations
{
    using MySql.Data.Entity;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public static class MigrationsRunner
    {
        public static void RunMigrations()
        {
            var configuration = new Configuration();
            var migrator = new DbMigrator(configuration);
            migrator.Update();
        }
    }

    internal sealed class Configuration : DbMigrationsConfiguration<GSDRequirementsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;

            DbConfiguration.SetConfiguration(new MySqlEFConfiguration());
            CodeGenerator = new MySql.Data.Entity.MySqlMigrationCodeGenerator();
            SetSqlGenerator("MySql.Data.MySqlClient", new MySqlMigrationSqlGenerator());
            SetHistoryContextFactory(MySqlProviderInvariantName.ProviderName, 
                                    (connection, schema) => new MySqlHistoryContext(connection, schema));
        }

        protected override void Seed(GSDRequirementsContext context)
        {
        }
    }
}
