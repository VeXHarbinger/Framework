﻿namespace Boilerplate.AspNetCore.TagHelpers.Twitter
{
    using System;
    using System.Text;
    using Microsoft.AspNetCore.Razor.TagHelpers;

    /// <summary>
    /// With Twitter Cards, you can attach rich photos, videos and media experience to Tweets that drive traffic to your
    /// website. Users who Tweet links to your content will have a "Card" added to the Tweet that's visible to all of
    /// their followers. See https://dev.twitter.com/cards/overview. Sign up for Twitter Card analytics to see who is
    /// sharing your site pages on Twitter. See https://analytics.twitter.com/about Validate your Twitter cards. See
    /// https://twitter.com/login?redirect_after_login=https%3A%2F%2Fcards-dev.twitter.com%2Fvalidator
    /// </summary>
    public abstract class TwitterCard : TagHelper
    {
        /// <summary>
        /// The username attribute name.
        /// </summary>
        protected const string UsernameAttributeName = "username";

        /// <summary>
        /// Gets the type of the Twitter card.
        /// </summary>
        /// <value>The type.</value>
        public abstract TwitterCardType Type { get; }

        /// <summary>
        /// Gets or sets the Twitter username associated with the page e.g. @RehanSaeedUK. This is a required property.
        /// </summary>
        /// <value>The Twitter username associated with the page e.g. @RehanSaeedUK. This is a required property.</value>
        [HtmlAttributeName(UsernameAttributeName)]
        public string Username { get; set; }

        /// <summary>
        /// Synchronously executes the <see cref="TagHelper"/> with the given <paramref name="context"/> and
        /// <paramref name="output"/>.
        /// </summary>
        /// <param name="context">Contains information associated with the current HTML tag.</param>
        /// <param name="output">A stateful HTML element used to generate an HTML tag.</param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Content.SetHtmlContent(this.ToString());
            output.TagName = null;
        }

        /// <summary>
        /// Returns a HTML-encoded <see cref="string"/> that represents this instance containing the Twitter card meta
        /// tags.
        /// </summary>
        /// <returns>
        /// A HTML-encoded <see cref="string"/> that represents this instance containing the Twitter card meta tags.
        /// </returns>
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            this.ToString(stringBuilder);
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Appends a HTML-encoded string representing this instance to the <paramref name="stringBuilder"/> containing
        /// the Twitter card meta tags.
        /// </summary>
        /// <param name="stringBuilder">The string builder.</param>
        public virtual void ToString(StringBuilder stringBuilder)
        {
            this.Validate();

            if (this.Type != TwitterCardType.Summary)
            {
                stringBuilder.AppendMetaNameContent("twitter:card", this.Type.ToTwitterString());
            }

            stringBuilder.AppendMetaNameContent("twitter:site", this.Username);
        }

        /// <summary>
        /// Checks that this instance is valid and throws exceptions if not valid.
        /// </summary>
        protected virtual void Validate()
        {
            if (this.Username == null)
            {
                throw new ArgumentNullException(nameof(this.Username));
            }
        }
    }
}