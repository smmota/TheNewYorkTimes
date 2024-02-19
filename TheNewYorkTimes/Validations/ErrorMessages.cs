namespace TheNewYorkTimes.Validations
{
    public static class ErrorMessages
    {
        public static string TituloObrigatorio => "O título é obrigatório.";
        public static string TituloTamanhoMaximo => "O título não pode ter mais de 50 caracteres.";
        public static string DescricaoObrigatoria => "A descrição é obrigatória.";
        public static string DescricaoTamanhoMaximo => "A descrição não pode ter mais de 500 caracteres.";
        public static string ChapeuObrigatoria => "O assunto da notícia é obrigatório.";
        public static string ChapeuTamanhoMaximo => "O assunto da notícia não pode ter mais de 20 caracteres.";
        public static string DataPublicacaoObrigatoria => "A data da publicação é obrigatória.";
        public static string DataPublicacaoMenorDataAtual => "A data da publicação não pode ser menor que a data atual.";
        public static string AutorObrigatorio => "O autor da notícia é obrigatório.";
        public static string AutorTamanhoMaximo => "O autor da notícia não pode ter mais de 50 caracteres.";
        public static string NomeObrigatorio => "O nome do usuário é obrigatório.";
        public static string NomeTamanhoMaximo => "O nome do usuário não pode ter mais de 50 caracteres.";
        public static string EmailObrigatorio => "O e-mail do usuário é obrigatório.";
        public static string EmailTamanhoMaximo => "O e-mail do usuário não pode ter mais de 50 caracteres.";
        public static string EmailFormatoInvalido => "O e-mail do usuário está no formato inválido.";
        public static string SenhaObrigatoria => "A senha do usuário é obrigatória.";
        public static string SenhaTamanhoMaximo => "A senha do usuário não pode ter mais de 10 caracteres.";
        public static string ConfirmeSenhaObrigatoria => "A confirmação da senha do usuário é obrigatória.";
        public static string ConfirmeSenhaTamanhoMaximo => "A confirmação da senha do usuário não pode ter mais de 10 caracteres.";
        public static string ConfirmeSenhaNaoConfere => "As senhas informadas não conferem.";
    }
}
