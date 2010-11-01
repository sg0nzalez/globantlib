<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <xsl:template match="BookRequests">
    <h2>Requested Books</h2>
    <ul>
      <xsl:for-each select="BookRequest">
        <li>
          <h3>
            <a title="Buscar" href="http://google.com/search?q={Title}" target="_blank">
              <xsl:value-of select="Title"/>
            </a>
          </h3>
          <h4>
            <xsl:value-of select="UserName"/>
          </h4>
        </li>
      </xsl:for-each>
    </ul>
  </xsl:template>

</xsl:stylesheet>