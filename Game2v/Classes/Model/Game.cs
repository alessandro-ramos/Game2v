using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Game2v.Model
{
    [Table("Games")]
    public class Game
    {
        [Key]       
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name="Id do Jogo")]
        [Required(ErrorMessage="Id do jogo obrigatório")]
        public int GameId { get; set; }
 
        [Display(Name="Título")]
        [Required(ErrorMessage="Título do Jogo obrigatório")]
        [StringLength(40, ErrorMessage="Título pode ter no máximo 40 letras")]
        public string GameTitle { get; set; }

        [Display(Name="Está com:")]
        public int? FriendId { get; set; }
        public Friend Friend { get; set; } = null;
    }
}