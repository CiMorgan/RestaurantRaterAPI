﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RestaurantRater.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2", "value3"};
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody] string value)    //does not return a value but will return a response code (i.e. 200)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)  //does not return a value but will return a response code (i.e. 200)
        {
        }

        // DELETE api/values/5   
        public void Delete(int id)   //does not return a value but will return a response code (i.e. 200)
        {
        }
    }
}
