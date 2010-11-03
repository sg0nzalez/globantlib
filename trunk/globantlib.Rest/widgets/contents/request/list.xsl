<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <xsl:template match="BookRequests">

    <div id="w-contents-request-list">
      <h2>Requested Titles</h2>
      <p>Click on the items to run a search of them on Google.</p>
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
    </div>

  </xsl:template>

</xsl:stylesheet>