using CourseWork.Lib.Entities;
using System;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using CourseWork.Lib;
using CourseWork.Lib.Models;
using System.Linq;
using System.Collections.Generic;

namespace CourseWork.API.Generators
{
    public class RestNlpGenerator : ITrajectoryGenerator
    {
        private readonly string endpoint;
        protected readonly CWContext db;

        public RestNlpGenerator(CWContext db)
        {
            endpoint = "http://localhost:3000/api/";
            this.db = db;
        }

        public Trajectory Generate(Specialization specialization, Guid userId, int size)
        {
            var client = MakeClient();

            var request = new RestRequest($"neighbors/{size}", DataFormat.Json);
            request.AddJsonBody(new NeighborQuery { Text = specialization.Name });

            var neighbors = client.Post<List<CourseNeighbor>>(request).Data;

            Trajectory trajectory = new() { Specialization = specialization, UserID = userId };
            trajectory.TrajectoryElements = neighbors
                .OrderBy(x => x.Dist)
                .Select((x, i) => 
                new TrajectoryElement
                {
                    Course = db.Courses.Find(x.ID),
                    Order = i,
                    TrajectoryID = trajectory.ID
                }).ToList();

            return trajectory;
        }

        private RestClient MakeClient()
        {
            var client = new RestClient(endpoint);
            client.UseNewtonsoftJson(Utils.Settings);
            return client;
        }
    }
}
