<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	
	<xsl:template match="/">
		<div class="thumbnail">
            <img src="{Content/Thumbnail}" />
            <ul>
                <xsl:for-each select="Content/Digitals/Digital">
                    <li><a href="#contents/download/{ID}">Download <xsl:value-of select="Format" /></a></li>
                </xsl:for-each>
                <xsl:for-each select="Content/physicals/physical">
                    <li><a href="#contents/calendar/{ID}">Get <xsl:value-of select="Type" /></a></li>
                </xsl:for-each>
            </ul>
        </div>
        <div class="contents">
            <h1 class="title"><xsl:value-of select="Content/Title" /></h1>
            <h2 class="tagline"><xsl:value-of select="Content/Publisher" /> <xsl:value-of select="Content/Author" /> <xsl:value-of select="Content/Released" /></h2>
            <p class="description"><xsl:value-of select="Content/Description" /></p>
        </div>
        <div class="reviews">
        </div>
	</xsl:template>
	
</xsl:stylesheet>