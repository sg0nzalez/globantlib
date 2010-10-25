<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	
	<xsl:template match="/">
		<div class="thumbnail">
            <img src="{Content/thumbnail}" />
            <ul>
                <xsl:for-each select="Content/digitals/digital">
                    <li><a href="#contents/download/{@id}">Download <xsl:value-of select="@format" /></a></li>
                </xsl:for-each>
                <xsl:for-each select="Content/physicals/physical">
                    <li><a href="#contents/calendar/{@id}">Get <xsl:value-of select="@type" /></a></li>
                </xsl:for-each>
            </ul>
        </div>
	</xsl:template>
	
</xsl:stylesheet>