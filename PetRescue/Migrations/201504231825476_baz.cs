namespace PetRescue.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPicUrl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pets", "PicURL", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pets", "PicURL");
        }
    }
}
