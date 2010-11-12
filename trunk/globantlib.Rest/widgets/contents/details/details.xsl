<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	
	<xsl:template match="Content">
    <div id="w-contents-details">
        <h1 class="title">
          <xsl:value-of select="Title" />
        </h1>
		    <div class="thumbnail">
            <img src="{Thumbnail}&amp;zoom=3" width="192" />
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
      <div id="w-contents-reviews">
        <div class="loading">Loading Reviews...</div>
      </div>
    </div>
	</xsl:template>

  <xsl:template match="Error">
    <p id="w-contents-details-error">
      <xsl:value-of select="Message"/><!-- The content you're trying to reach doesn't exist or has been removed. -->
    </p>
  </xsl:template>
	
</xsl:stylesheet>