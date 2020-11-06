using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Game2v.Model
{
    [Table("Friends")]
    public class Friend
    {
        [Key]       
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name="Id do Amigo")]
        [Required(ErrorMessage="Id do amigo obrigatório")]
        public int FriendId { get; set; }
 
        [Display(Name="Nome")]
        [Required(ErrorMessage="Nome do amigo obrigatório")]
        [StringLength(60, ErrorMessage="Nome pode ter no máximo 60 letras")]
        public string FriendName { get; set; }

        public List<Game> Games { get; set; }    
    }
}