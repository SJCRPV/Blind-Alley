using Android.Runtime;
using SQLite;

namespace Blind_Alley
{
    [Table("Highscore")]
    class Highscore
    {
        public int bestScore { get; set; }
    }
}