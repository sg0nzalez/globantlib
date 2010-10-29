<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <xsl:template match="BookRequests">
    <ul>
      <xsl:for-each select="BookRequest">
        <li>
          <h3>
            <xsl:value-of select="Title"/>
          </h3>
          <h4>
            <xsl:value-of select="Username"/>
          </h4>
        </li>
      </xsl:for-each>
    </ul>
  </xsl:template>

</xsl:stylesheet>