using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Data.Migration;

namespace MainBit.Relationships
{
    public class Migrations : DataMigrationImpl {

        public int Create() {
            SchemaBuilder.CreateTable("RelationshipRecord",
                table => table
                    .Column<int>("Id", column => column.PrimaryKey().Identity())
                    .Column<int>("RelationshipGroupRecord_Id")
                    .Column<int>("ContentItemRecord1_Id")
                    .Column<int>("ContentItemRecord2_Id")
                );

            return 1;
        }
    }
}