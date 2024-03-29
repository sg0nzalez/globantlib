<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <xsl:template match="/">
    <!-- .tab-buttons -->
      <div class="reviews">
        <h2>
          Reviews <a id="w-contents-new-review-show" href="#contents/submit_review/{ID}">Submit your own review</a>
        </h2>

        <!-- new review form -->
        <form id="w-contents-new-review-form" action="#">
          <div class="new-review">
            <div class="field">
              <label for="w-contents-review-title">Title:</label>
              <input type="text" id="w-contents-review-title" />
            </div>
            <div class="field">
              <label for="w-contents-review-rate">Rating:</label>
              <select id="w-contents-review-rate">
                <option>1</option>
                <option>2</option>
                <option>3</option>
                <option>4</option>
                <option selected="selected">5</option>
              </select>
            </div>
            <div class="field">
              <label for="w-contents-review-text">Comment:</label>
              <textarea id="w-contents-review-text"></textarea>
            </div>
            <div class="field submit">
              <input type="submit" value="Submit" />
              <a href="#" id="w-contents-new-review-hide">Cancel</a>
            </div>
          </div>
        </form>
        <!-- new review form -->

        <ul>
          <xsl:for-each select="Reviews/Review">
          <li>
            <div class="head">
              <img width="50" src="{User/Thumbnail}" />
              <h4>
                <xsl:value-of select="User/Name"/>
              </h4>
              <h5>
                <xsl:value-of select="Submitted"/>
              </h5>
            </div>
            <div class="body">
              <h3>
                <xsl:value-of select="Title"/>
                <img width="70" src="img/stars{Rate}.png" />
              </h3>

              <p>
                <xsl:value-of select="Comment"/>
              </p>
            </div>
          </li>
          </xsl:for-each>
        </ul>

        <xsl:if test="count(Error) > 0">
          <xsl:for-each select="Error">
          <p id="w-contents-reviews-error">
            <xsl:value-of select="Message"/>
            <!-- No reviews just yet. -->
          </p>
          </xsl:for-each>
        </xsl:if>
        
      </div>
    <!-- .tab-contents -->
  </xsl:template>

</xsl:stylesheet>