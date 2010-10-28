<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	
	<xsl:template match="Content">
    <div id="w-contents-details">
        <h1 class="title">
          <xsl:value-of select="Title" />
        </h1>
		    <div class="thumbnail">
            <img src="{Thumbnail}" width="192" />
            <ul>
                <xsl:for-each select="Digitals/Digital">
                    <li><a target="_blank" href="{Link}">Download <xsl:value-of select="Format" /></a></li>
                </xsl:for-each>
                <xsl:for-each select="Physicals/Physical">
                    <li><a href="#contents/calendar/{ID}">Get <xsl:value-of select="Type" /></a></li>
                </xsl:for-each>
            </ul>
        </div>
        <div class="contents">
            <ul class="data">
              <xsl:if test="Publisher != ''">
                <li>
                  <span>Publisher:</span>
                  <xsl:value-of select="Publisher"/>
                </li>
              </xsl:if>
              <xsl:if test="Author != ''">
                <li>
                  <span>Author:</span>
                  <xsl:value-of select="Author"/>
                </li>
              </xsl:if>
              <xsl:if test="Released != ''">
                <li>
                  <span>Released:</span>
                  <xsl:value-of select="Released"/>
                </li>
              </xsl:if>
              <xsl:if test="ISBN10 != ''">
                <li>
                  <span>ISBN-10:</span>
                  <xsl:value-of select="ISBN10"/>
                </li>
              </xsl:if>
              <xsl:if test="ISBN13 != ''">
                <li>
                  <span>ISBN-13:</span>
                  <xsl:value-of select="ISBN13"/>
                </li>
              </xsl:if>
            </ul>
            <p class="description"><xsl:value-of select="Description" /></p>
        </div>
      <div class="reviews">
        <!-- placeholder for tabs... someday we'll need them -->
        <ul class="tab-buttons">
          
        </ul> <!-- .tab-buttons -->
        <div class="tab-contents">
          <div class="reviews">
            <h2>Reviews <a id="w-contents-new-review-show" href="#contents/submit_review/{ID}">Submit your own review</a></h2>

            <!-- new review form -->
            <form id="w-contents-new-review-form">
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
                    <option>5</option>
                  </select>
                </div>
                <div class="field">
                  <label for="w-contents-review-text">Comment:</label>
                  <textarea id="w-contents-review-text"></textarea>
                </div>
                <div class="field submit">
                  <input type="submit" value="Submit" />
                </div>
              </div>
            </form>
            <!-- new review form -->
            
            <ul>
              <xsl:for-each select="Reviews/Review">
                <li>
                  <div class="head">
                    <img src="{User/Thumbnail}" />
                    <h4>
                      <xsl:value-of select="User/Name" />
                    </h4>                    
                  </div>
                  <div class="body">
                    <h3>
                      <xsl:value-of select="Title" />
                    </h3>
                    <img src="img/stars5.gif" />
                    <p>
                      <xsl:value-of select="Submitted" />
                    </p>
                    <p>
                      <xsl:value-of select="Comment" />
                    </p>
                  </div>
                </li>
              </xsl:for-each>
            </ul>
          </div>
        </div> <!-- .tab-contents -->
      </div>
    </div>
	</xsl:template>

  <xsl:template match="Error">
    <p id="w-contents-details-error">
      <xsl:value-of select="Message"/><!-- The content you're trying to reach doesn't exist or has been removed. -->
    </p>
  </xsl:template>
	
</xsl:stylesheet>