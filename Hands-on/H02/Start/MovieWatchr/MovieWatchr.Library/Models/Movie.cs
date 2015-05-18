using System.Runtime.Serialization;

namespace MovieWatchr.Models
{
    [DataContract]
    public class Movie
    {
        [DataMember(Name = "adult")]
        public bool IsAdult { get; set; }
        [DataMember(Name = "backdrop_path")]
        public string BackdropPath { get; set; }
        [DataMember(Name = "belongs_to_collection")]
        public object BelongsToCollection { get; set; }
        [DataMember(Name = "budget")]
        public int Budget { get; set; }
        [DataMember(Name = "genres")]
        public Genre[] Genres { get; set; }
        [DataMember(Name = "homepage")]
        public string Homepage { get; set; }
        [DataMember(Name = "id")]
        public int Id { get; set; }
        [DataMember(Name = "imdb_id")]
        public string ImdbId { get; set; }
        [DataMember(Name = "original_language")]
        public string OriginalLanguage { get; set; }
        [DataMember(Name = "original_title")]
        public string OriginalTitle { get; set; }
        [DataMember(Name = "overview")]
        public string Overview { get; set; }
        [DataMember(Name = "popularity")]
        public float Popularity { get; set; }
        [DataMember(Name = "poster_path")]
        public string PosterPath { get; set; }
        [DataMember(Name = "production_companies")]
        public Production_Companies[] ProductionCompanies { get; set; }
        [DataMember(Name = "production_countries")]
        public Production_Countries[] ProductionCountries { get; set; }
        [DataMember(Name = "release_date")]
        public string ReleaseDate { get; set; }
        [DataMember(Name = "revenue")]
        public int Revenue { get; set; }
        [DataMember(Name = "runtime")]
        public int Runtime { get; set; }
        [DataMember(Name = "spoken_languages")]
        public Spoken_Languages[] SpokenLanguages { get; set; }
        [DataMember(Name = "status")]
        public string Status { get; set; }
        [DataMember(Name = "tagline")]
        public string Tagline { get; set; }
        [DataMember(Name = "title")]
        public string Title { get; set; }
        [DataMember(Name = "video")]
        public bool Video { get; set; }
        [DataMember(Name = "vote_average")]
        public float VoteAverage { get; set; }
        [DataMember(Name = "vote_count")]
        public int VoteCount { get; set; }

        [DataMember(Name = "rating")]
        public double Rating { get; set; }
        [DataMember(Name = "watched")]
        public bool IsWatched { get; set; }
    }

    public class Genre
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }
        [DataMember(Name = "name")]
        public string Name { get; set; }
    }

    public class Production_Companies
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }
        [DataMember(Name = "id")]
        public int Id { get; set; }
    }

    public class Production_Countries
    {
        [DataMember(Name = "iso_3166_1")]
        public string Iso31661 { get; set; }
        [DataMember(Name = "name")]
        public string Name { get; set; }
    }

    public class Spoken_Languages
    {
        [DataMember(Name = "iso_639_1")]
        public string Iso6391 { get; set; }
        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}
