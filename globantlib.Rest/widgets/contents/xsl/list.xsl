<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:template match="/">
		<ul class="contents-list">
			<xsl:for-each select="Response/ArrayOfContents/Content">
				<li class="content">
					<a class="thumbnail" href="#contents/details/{ID}"><img width="96" src="img/{thumbnail}" /></a>
					<h2 class="title"><a href="#contents/details/{ID}"><xsl:value-of select="Title" /></a></h2>
					<h3 class="tagline"><xsl:value-of select="Publisher" /> <xsl:value-of select="Author" /> <xsl:value-of select="Released" /></h3>
					<p class="available">
						<xsl:if test="hasDigital = 'Yes'">Available in digital</xsl:if>
						<xsl:if test="hasPhysical = 'Yes'">Available in print</xsl:if>
					</p>
					<p class="description"><xsl:value-of select="Description" /></p>
				</li>
			</xsl:for-each>
		</ul>
		<p class="contents-pagination">
			<xsl:for-each select="Response/Pages/Page">
				<xsl:choose>
					<xsl:when test="current = 'true'">
						<b><xsl:value-of select="number" /></b>
					</xsl:when>
					<xsl:otherwise>
						<a href="#contents/list/{number}"><xsl:value-of select="number" /></a>
					</xsl:otherwise>
				</xsl:choose>
			</xsl:for-each>
		</p>
	</xsl:template>
	
</xsl:stylesheet>