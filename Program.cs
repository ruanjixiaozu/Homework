using Mono.Data.Sqlite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SqliteManager : MonoBehaviour
{

    void Start()
    {

        SqliteDbHelper db = new SqliteDbHelper("data source=scoreDB.db");

        //   db.InsertInto("leaderboard",3,50);



        //然后在删掉两条数据
        //db.Delete("biao", new string[] { "qq", "qq" }, new string[] { "'123456789'", "'223456789'" });
        //动态查询数据信息
        //  SqliteDataReader sqReader = db.SelectWhere("biao", new string[] { "name", "email" }, new string[] { "qq" }, new string[] { "=" }, new string[] { "123456789" });


        //while (sqReader.Read())                 //表名，查询数据集合，字段，操作，值
        //{
        //    Debug.Log(sqReader.GetString(sqReader.GetOrdinal("name")) + sqReader.GetString(sqReader.GetOrdinal("email")));
        //}
        //删除表
        //db.DeleteContents("biao");
        //关闭对象
        db.CloseSqlConnection();
    }
}
