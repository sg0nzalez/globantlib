<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	
	<xsl:template match="/">
		<ul class="contents-list">
			<xsl:for-each select="Response/ArrayOfContents/Content">
				<li class="content">
					<a class="thumbnail" href="#contents/details/{id}"><img src="img/{thumbnail}" /></a>
					<h2 class="title"><xsl:value-of select="title" /></h2>
					<h3 class="tagline"><xsl:value-of select="publisher" /> <xsl:value-of select="author" /> <xsl:value-of select="released" /></h3>
					<p>
						<xsl:if test="@hasDigital = 'Yes'">Available in digital</xsl:if>
						<xsl:if test="@hasPhysical = 'Yes'">Available in print</xsl:if>
					</p>
					<p class="description"><xsl:value-of select="description" /></p>
				</li>
			</xsl:for-each>
		</ul>
		<p class="contents-pagination">
			<xsl:for-each select="Response/Pages/Page">
				<xsl:choose>
					<xsl:when test="@current = 'Yes'">
						<b><xsl:value-of select="@number" /></b>
					</xsl:when>
					<xsl:otherwise>
						<a href="#contents/list/{@number}"><xsl:value-of select="@number" /></a>
					</xsl:otherwise>
				</xsl:choose>
			</xsl:for-each>
		</p>
	</xsl:template>
	
</xsl:stylesheet>