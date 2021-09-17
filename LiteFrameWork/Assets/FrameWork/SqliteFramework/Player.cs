using System;
using System.Collections.Generic;

namespace LiteSqlite
{
    public class Player : SqlDataBase
    {
        public string account;
        public string passward;
        
        public Player(string account, string passward)
        {
            this.account = account;
            this.passward = passward;
        }

        public override string[] NameToArray()
        {
            return new string[] {"id", "account", "passward"};
        }

        public override string[] TypeToArray()
        {
            return new string[] {"int", "string", "string"};
        }

        public override object[] DataToArray()
        {
            return new object[] {id,account,passward };
        }

        public override SqlDataBase InitWithSqlData(Dictionary<string, object> _data)
        {
            this.id = System.Convert.ToInt32(_data["id"]);
            this.account = System.Convert.ToString(_data["account"]);
            this.passward = System.Convert.ToString(_data["passward"]);

            return this;
        }
    }
}
