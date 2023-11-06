using Coravel.Mailer.Mail;
using AprenderBrincando.Models;

namespace AprenderBrincando.Messages
{
    public class SendEmail : Mailable<Email>
    {
        private Email email;

        public SendEmail(Email email) => this.email = email;

        public override void Build()
        {
            this.To(this.email.Para)
                .From(new MailRecipient("odnanref.puc@gmail.com", "Fernando David"))
                .Subject(this.email.Assunto)
                .View("~/Views/Email/Conteudo.cshtml", this.email);
        }
    }
}
