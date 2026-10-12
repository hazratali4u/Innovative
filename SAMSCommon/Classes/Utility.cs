using System;
using System.Data;

namespace  SAMSCommon.Classes
{
    public static class Utility
    {
        public static bool SendEmail(string p_Email_From, string p_Email_To, string p_SMTP, string p_SMTPPassword, string p_Email_Subject, string p_Email_Body)
        {
            try
            {
                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(p_Email_From, p_SMTPPassword);

                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();

                message.From = new System.Net.Mail.MailAddress(p_Email_From, "NEON");

                System.Net.Mail.MailAddress to = new System.Net.Mail.MailAddress(p_Email_To);
                message.To.Add(to);
                
                message.IsBodyHtml = true;
                message.Subject = p_Email_Subject;
                message.Body = p_Email_Body;
                message.IsBodyHtml = true;
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(p_SMTP);
                
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = credentials;
                smtp.EnableSsl = true;
                smtp.Send(message);

                return true;
            }
            catch (Exception ex)
            {
                ExceptionPublisher.PublishException(ex);
                return false;
            }
        }

        public static bool SendEmail(string p_Email_From, string p_Email_To, string[] p_Email_Cc, string p_SMTP, string p_SMTPPassword, string p_Email_Subject, string p_Email_Body, string p_Attachment, int p_Port)
        {
            try
            {
                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(p_Email_From, p_SMTPPassword);

                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();

                message.From = new System.Net.Mail.MailAddress(p_Email_From, "NEON");

                System.Net.Mail.MailAddress to = new System.Net.Mail.MailAddress(p_Email_To);
                message.To.Add(to);

                foreach (string strEmaiCc in p_Email_Cc)
                {
                    if (strEmaiCc != "")
                    {
                        System.Net.Mail.MailAddress Cc = new System.Net.Mail.MailAddress(strEmaiCc);
                        message.CC.Add(Cc);
                    }
                }

                message.Attachments.Add(new System.Net.Mail.Attachment(p_Attachment));
                
                message.IsBodyHtml = true;
                message.Subject = p_Email_Subject;
                message.Body = p_Email_Body;
                message.IsBodyHtml = true;
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(p_SMTP);

                smtp.UseDefaultCredentials = false;
                smtp.Port = p_Port;
                smtp.Credentials = credentials;
                smtp.EnableSsl = true;
                smtp.Send(message);

                return true;
            }
            catch (Exception ex)
            {
                ExceptionPublisher.PublishException(ex);
                return false;
            }
        }

        public static bool SendEmail(string p_Email_From, DataTable p_Email_To, string p_SMTP, string p_SMTPPassword, string p_Email_Subject, string p_Email_Body)
        {
            try
            {
                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(p_Email_From, p_SMTPPassword);

                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();

                message.From = new System.Net.Mail.MailAddress(p_Email_From, "NEON");

                foreach (DataRow dr in p_Email_To.Rows)
                {
                    if (dr["EMAIL"].ToString() != "")
                    {
                        System.Net.Mail.MailAddress to = new System.Net.Mail.MailAddress(dr["EMAIL"].ToString());
                        message.To.Add(to);
                    }
                }
                message.IsBodyHtml = true;
                message.Subject = p_Email_Subject;
                message.Body = p_Email_Body;
                message.IsBodyHtml = true;
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(p_SMTP);

                smtp.UseDefaultCredentials = false;
                smtp.Credentials = credentials;
                smtp.EnableSsl = true;
                smtp.Send(message);

                return true;
            }
            catch (Exception ex)
            {
                ExceptionPublisher.PublishException(ex);
                return false;
            }
        }

        public static bool SendEmail(string p_Email_From, DataTable p_Email_To, string[] p_Email_Cc, string p_SMTP, string p_SMTPPassword, string p_Email_Subject, string p_Email_Body, int p_Port)
        {
            try
            {
                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(p_Email_From, p_SMTPPassword);

                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();

                message.From = new System.Net.Mail.MailAddress(p_Email_From, "NEON");

                foreach (DataRow dr in p_Email_To.Rows)
                {
                    if (dr["EMAIL"].ToString() != "")
                    {
                        System.Net.Mail.MailAddress to = new System.Net.Mail.MailAddress(dr["EMAIL"].ToString());
                        message.To.Add(to);
                    }
                }

                foreach (string strEmaiCc in p_Email_Cc)
                {
                    if (strEmaiCc != "")
                    {
                        System.Net.Mail.MailAddress Cc = new System.Net.Mail.MailAddress(strEmaiCc);
                        message.CC.Add(Cc);
                    }
                }

                message.IsBodyHtml = true;
                message.Subject = p_Email_Subject;
                message.Body = p_Email_Body;
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(p_SMTP);

                smtp.UseDefaultCredentials = false;
                smtp.Port = p_Port;                
                smtp.Credentials = credentials;
                smtp.EnableSsl = true;
                smtp.Send(message);

                return true;
            }
            catch (Exception ex)
            {
                ExceptionPublisher.PublishException(ex);
                return false;
            }
        }

        public static bool SendEmail(string p_Email_From, DataTable p_Email_To, DataTable p_Email_Cc, string p_SMTP, string p_SMTPPassword, string p_Email_Subject, string p_Email_Body, string p_Attachment, int p_Port)
        {
            try
            {
                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(p_Email_From, p_SMTPPassword);

                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();

                message.From = new System.Net.Mail.MailAddress(p_Email_From, "NEON");

                foreach (DataRow dr in p_Email_To.Rows)
                {
                    if (dr["EMAIL"].ToString() != "")
                    {
                        System.Net.Mail.MailAddress to = new System.Net.Mail.MailAddress(dr["EMAIL"].ToString());
                        message.To.Add(to);
                    }
                }


                foreach (DataRow dr in p_Email_Cc.Rows)
                {
                    if (dr["EMAIL"].ToString() != "")
                    {
                        System.Net.Mail.MailAddress cc = new System.Net.Mail.MailAddress(dr["EMAIL"].ToString());
                        message.CC.Add(cc);
                    }
                }

                message.Attachments.Add(new System.Net.Mail.Attachment(p_Attachment));

                message.IsBodyHtml = true;
                message.Subject = p_Email_Subject;
                message.Body = p_Email_Body;
                message.IsBodyHtml = true;
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(p_SMTP);

                smtp.UseDefaultCredentials = false;
                smtp.Port = p_Port;
                smtp.Credentials = credentials;
                smtp.EnableSsl = true;
                smtp.Send(message);

                return true;
            }
            catch (Exception ex)
            {
                ExceptionPublisher.PublishException(ex);
                return false;
            }
        }

        public static bool SendEmail(string p_Email_From, DataTable p_Email_To, DataTable p_Email_Cc, string p_SMTP, string p_SMTPPassword, string p_Email_Subject, string p_Email_Body, DataTable p_Attachment, int p_Port)
        {
            try
            {
                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(p_Email_From, p_SMTPPassword);

                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();

                message.From = new System.Net.Mail.MailAddress(p_Email_From, "NEON");

                foreach (DataRow dr in p_Email_To.Rows)
                {
                    if (dr["EMAIL_ID"].ToString() != "")
                    {
                        System.Net.Mail.MailAddress to = new System.Net.Mail.MailAddress(dr["EMAIL_ID"].ToString());
                        message.To.Add(to);
                    }
                }


                foreach (DataRow dr in p_Email_Cc.Rows)
                {
                    if (dr["CC"].ToString() != "")
                    {
                        System.Net.Mail.MailAddress cc = new System.Net.Mail.MailAddress(dr["CC"].ToString());
                        message.CC.Add(cc);
                    }
                }

                foreach (DataRow dr in p_Attachment.Rows)
                {
                    if (dr["PATH"].ToString() != "")
                    {
                        message.Attachments.Add(new System.Net.Mail.Attachment(dr["PATH"].ToString()));
                    }
                }

               

                message.IsBodyHtml = true;
                message.Subject = p_Email_Subject;
                message.Body = p_Email_Body;
                message.IsBodyHtml = true;
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(p_SMTP);

                smtp.UseDefaultCredentials = false;
                smtp.Port = p_Port;
                smtp.Credentials = credentials;
                smtp.EnableSsl = true;
                smtp.Send(message);

                return true;
            }
            catch (Exception ex)
            {
                ExceptionPublisher.PublishException(ex);
                return false;
            }
        }


        public static string ShowAlert(bool IsSuccess,string Message)
        {
            string AlertMessage = "";

            if (IsSuccess)
            {
                AlertMessage = "<div style='padding:10px; margin:10px; background-color:#ecfaf0;border: Solid 1px #04551d; color:#04551d;	font-size:12px; font-family:Verdana;'>" + Message + "</div>";
            }
            else
            {
                AlertMessage = "<div style='padding:10px; margin:10px; background-color:#fbefef;	border: Solid 1px #bb0000; color:#bb0000;	font-size:12px; font-family:Verdana;'>" + Message + "</div>";
            }

            return AlertMessage;
        }
    }
}

