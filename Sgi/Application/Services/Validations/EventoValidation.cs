using Sgi.Application.Dtos;
using Sgi.CrossCutting.Constants;

namespace Sgi.Application.Services.Validations
{
    public static class EventoValidation
    {
        public static string ValidarEvento(EventoDto eventoDto)
        {
            if (eventoDto.Nome == null || eventoDto.Nome.Length < 2 || eventoDto.Nome.Length > 200)
                return "Nome do evento não pode ser nulo e deve ter entre 2 e 200 caracteres";

            if (eventoDto.Tipo == null || (eventoDto.Tipo != TipoEventoConst.Filme && eventoDto.Tipo != TipoEventoConst.Show &&
                    eventoDto.Tipo != TipoEventoConst.PecaTeatro && eventoDto.Tipo != TipoEventoConst.Palestra))
                return "Tipo do evento deve ser filme, show, peça de teatro ou palestra";

            if (eventoDto.Modalidade == null || (eventoDto.Modalidade != ModalidadeConst.Presencial && eventoDto.Modalidade != ModalidadeConst.Online))
                return "Modalidade deve ser presencial ou online";

            if (eventoDto.Descricao == null || eventoDto.Descricao.Length < 5 || eventoDto.Descricao.Length > 300)
                return "Descrição não pode ser nulo ou vazio e deve ter entre 5 e 300 caracteres";

            if (eventoDto.LinkStream != null && eventoDto.LinkStream.Length > 500)
                return "Link da stream não pode ter mais de 500 caracteres";

            if (eventoDto.LinkRedirecionamento != null && eventoDto.LinkRedirecionamento.Length > 500)
                return "Link de redirecionamento não pode ter mais de 500 caracteres";

            if (eventoDto.Preco < 0)
                return "Preço do evento não pode ser negativo";

            return null;
        }
    }
}
