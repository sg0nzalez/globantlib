<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	
	<xsl:template match="Response">
    
    <p id="w-contents-pagination">
      <xsl:for-each select="Pages/Page">
        <xsl:choose>
          <xsl:when test="current = 'true'">
            <b>
              <xsl:value-of select="number" />
            </b> 
          </xsl:when>
          <xsl:otherwise>
            <a href="#contents/list/{number}">
              <xsl:value-of select="number" />
            </a> 
          </xsl:otherwise>
        </xsl:choose>
      </xsl:for-each>
    </p>
    
    <ul id="w-contents-list">
			<xsl:for-each select="ArrayOfContents/Content">
				<li class="content">
					<a class="thumbnail" href="#contents/details/{ID}"><img width="100" src="{Thumbnail}&amp;zoom=2" /></a>
					<h2 class="title"><a href="#contents/details/{ID}"><xsl:value-of select="Title" /></a></h2>
					<h3 class="tagline">
            <xsl:if test="Publisher != ''"><span><xsl:value-of select="Publisher" /></span></xsl:if>
            <xsl:if test="Author != ''"><span><xsl:value-of select="Author" /></span></xsl:if>
            <xsl:if test="Released != ''"><span><xsl:value-of select="Released" /></span></xsl:if>
          </h3>
					<p class="available">
						<xsl:if test="hasDigital = 'Yes'"><a href="#contents/details/{ID}" class="digital">Available in digital</a></xsl:if>
						<xsl:if test="hasPhysical = 'Yes'"><a href="#contents/calendar/{ID}" class="physical">Available for rent</a></xsl:if>
					</p>
					<p class="description"><xsl:value-of select="Description" /></p>
				</li>
			</xsl:for-each>
		</ul>
    
  </xsl:template>

  <xsl:template match="Error">
    
    <p id="w-contents-search-error">
      <xsl:value-of select="Message"/>
    </p>
    
  </xsl:template>
  
</xsl:stylesheet>