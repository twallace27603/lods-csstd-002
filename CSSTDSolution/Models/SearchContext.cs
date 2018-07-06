using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CSSTDEvaluation;
using CSSTDModels;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Common;
using Microsoft.Azure.Search.Models;

namespace CSSTDSolution.Models
{
    public class SearchContext : ISearchContext
    {
        /*
         *                 string searchServiceName = data.SearchName;

                string queryApiKey = data.SearchKey;

                SearchIndexClient indexClient = new SearchIndexClient(searchServiceName, "documentdb-index", new SearchCredentials(queryApiKey));
                SearchParameters parameters;
 

                parameters =
                    new SearchParameters()
                    {
                        Select = new[] { "PropertyID", "Street", "City", "State", "PostalCode", "Description", "Price" },
                        Filter = "Price lt 900000000000.00"
                    };

                DocumentSearchResult<Listing> results = indexClient.Documents.Search<Listing>("*", parameters);
                foreach(SearchResult<Listing> document in results.Results)
                {
                    result.SearchResults.Add(document.Document);
                }
                if (result.SearchResults.Count > 0)
                {
                    result.Passed = true;
                    result.Status = "Search is properly configured";
                } else
                {
                    result.Passed = false;
                    result.Status = "Search returned zero results";
                }
         * 
         * */
        private SearchIndexClient indexClient;
        public SearchContext(string connectionString)
        {
            var credentials = connectionString.Split(';');
            var searchServiceName = credentials[0];
            var queryApiKey = credentials[1];
            indexClient = new SearchIndexClient(searchServiceName, "documentdb-index", new SearchCredentials(queryApiKey));

            this.ConnectionString = connectionString;
        }

        public string ConnectionString { get; set; }

        public  void CreateIndex()
        {
            throw new NotImplementedException();
        }

        public  List<ProductDocument> GetDocuments(string Industry)
        {
            var result = new List<ProductDocument>();
            SearchParameters parameters =
                new SearchParameters()
                {
                    Select = new[] { "Industry", "Name", "Tier", "Description" },
                    Filter = "Tier eq 'Basic'"
                };
            DocumentSearchResult<ProductDocument> documents = indexClient.Documents.Search<ProductDocument>("*", parameters);
            foreach(var document in documents.Results)
            {
                result.Add(document.Document);

            }

            return result;

        }
    }
}