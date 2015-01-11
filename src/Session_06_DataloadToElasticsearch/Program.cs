using Nest;
using Session_06_DataloadToElasticsearch.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Session_06_DataloadToElasticsearch
{
    class Program
    {
        static void Main(string[] args)
        {
            //設定與Elasticsearch的連線
            var node = new Uri("http://localhost:9200");
            var settings = new ConnectionSettings(node, defaultIndex: "stackoverflow");
            var esClient = new ElasticClient(settings);

            //設定要import到Elasticsearch的資料檔
            string xmlDataFile = @"D:\temp\datadump\apple\Posts.xml";

            XmlReader xmlReader = XmlReader.Create(xmlDataFile);
            int rowCount = 0;

            var stopWatch = new Stopwatch();
            stopWatch.Start(); //開始計時

            var bulkDescriptor = new BulkDescriptor();
            var batchCount = 0;

            //開始parsing xml資料檔與把資料import到Elasticsearch
            while (xmlReader.Read())
            {
                if (xmlReader.IsStartElement())
                {
                    //取得Xml的element name
                    if (xmlReader.Name.Equals("row"))
                    {
                        rowCount++;
                        var post = ParseingXmlAndGetPost(xmlReader);
                        //把Post放到BulkDescriptor來作為批次上傳
                        bulkDescriptor.Index<Post>(op => op.Document(post));

                        //每一萬筆一次性的Import到Elasticsearch中, 來增加效率
                        if (rowCount % 10000 == 0) {
                            batchCount++;

                            var stopwatch2 = new Stopwatch();
                            stopwatch2.Start();

                            var bulkResult = esClient.Bulk(bulkDescriptor);

                            stopwatch2.Stop();

                            if (bulkResult.IsValid)
                                Console.WriteLine("Index batch #[" + (rowCount-10000) + " ~ " + rowCount + "] is ok, spends " + stopwatch2.ElapsedMilliseconds + " ms!");
                            else
                                Console.WriteLine("Index batch #[" + (rowCount - 10000) + " ~ " + rowCount + "] is fail, spends " + stopwatch2.ElapsedMilliseconds + " ms!");

                            bulkDescriptor = new BulkDescriptor(); //產生一個新的 BulkDescriptor
                        }
                    }
                }
            }

            var stopwatch3 = new Stopwatch();
            stopwatch3.Start();

            var bulkResult2 = esClient.Bulk(bulkDescriptor);

            stopwatch3.Stop();

            if (bulkResult2.IsValid)
                Console.WriteLine("Index batch #[" + batchCount * 10000 + " ~ " + rowCount + "] is ok, spends " + stopwatch3.ElapsedMilliseconds + " ms!");
            else
                Console.WriteLine("Index batch #[" + batchCount * 10000 + " ~ " + rowCount + "] is fail, spends " + stopwatch3.ElapsedMilliseconds + " ms!");

            
            stopWatch.Stop();

            Console.WriteLine("Total process [" + rowCount + "] records, spends " + stopWatch.ElapsedMilliseconds /1000 + " seconds!");

            Console.ReadLine();
        }

        static Post ParseingXmlAndGetPost(XmlReader xmlReader)
        {
            var post = new Post();

            post.Id = xmlReader["Id"] == null ? 0 : int.Parse(xmlReader["Id"]);

            post.PostTypeId = xmlReader["PostTypeId"] == null ? 0 : int.Parse(xmlReader["PostTypeId"]);

            //轉換PostTypeId成為字串
            switch (post.PostTypeId)
            {
                case 1:
                    post.PostType = "Question";
                    break;
                case 2:
                    post.PostType = "Answer";
                    break;
                default:
                    post.PostType = "Unknown";
                    break;
            }

            if (xmlReader["AcceptedAnswerId"] != null)
                post.AcceptedAnswerId = int.Parse(xmlReader["AcceptedAnswerId"]);

            if (xmlReader["ParentId"] != null)
                post.ParentId = int.Parse(xmlReader["ParentId"]);

            var CreationDate_Str = xmlReader["CreationDate"];
            post.CreationDate = DateTime.Parse(CreationDate_Str);

            post.Score = xmlReader["Score"] == null ? 0 : int.Parse(xmlReader["Score"]);
            post.ViewCount = xmlReader["ViewCount"] == null ? 0 : int.Parse(xmlReader["ViewCount"]);
            post.Body = xmlReader["Body"];
            post.OwnerUserId = xmlReader["OwnerUserId"] == null ? 0 : int.Parse(xmlReader["OwnerUserId"]);
            post.OwnerDisplayName = xmlReader["OwnerDisplayName"];
            post.LastEditorUserId = xmlReader["LastEditorUserId"] == null ? 0 : int.Parse(xmlReader["LastEditorUserId"]);
            post.LastEditorDisplayName = xmlReader["LastEditorDisplayName"];

            var LastEditDate_Str = xmlReader["LastEditDate"];
            if (LastEditDate_Str != null)
                post.LastEditDate = DateTime.Parse(LastEditDate_Str);

            var LastActivityDate_Str = xmlReader["LastActivityDate"];
            if (LastActivityDate_Str != null)
                post.LastActivityDate = DateTime.Parse(LastActivityDate_Str);

            post.Title = xmlReader["Title"];
            post.Tags_String = xmlReader["Tags"];

            post.Tags = ParseingTags(post.Tags_String);

            post.AnswerCount = xmlReader["AnswerCount"] == null ? 0 : int.Parse(xmlReader["AnswerCount"]);
            post.CommentCount = xmlReader["CommentCount"] == null ? 0 : int.Parse(xmlReader["CommentCount"]);
            post.FavoriteCount = xmlReader["FavoriteCount"] == null ? 0 : int.Parse(xmlReader["FavoriteCount"]);

            var ClosedDate_Str = xmlReader["ClosedDate"];
            if (ClosedDate_Str != null)
                post.ClosedDate = DateTime.Parse(ClosedDate_Str);

            var CommunityOwnedDate_Str = xmlReader["CommunityOwnedDate"];
            if (CommunityOwnedDate_Str != null)
                post.CommunityOwnedDate = DateTime.Parse(CommunityOwnedDate_Str);

            return post;
        }

        static string[] ParseingTags(string tagsTring)
        {
            if (tagsTring == null)
                return null;

            //<tag1><tag2>....
            var splits = tagsTring.Split('>');
            var result = new List<string>();
            for (var i = 0; i < splits.Count(); i++)
            {
                if (splits[i] != null && splits[i].Length > 0)
                    result.Add(splits[i].Substring(1));
            }
            return result.ToArray();
        }

    }
}
