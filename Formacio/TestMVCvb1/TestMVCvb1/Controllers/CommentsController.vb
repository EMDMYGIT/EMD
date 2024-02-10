Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.Entity
Imports System.Linq
Imports System.Net
Imports System.Web
Imports System.Web.Mvc

Namespace TestMVCvb1
    Public Class CommentsController
        Inherits System.Web.Mvc.Controller

        Private db As New TESTEntities

        ' GET: Comments
        Function Index() As ActionResult
            Return View(db.TB_Comments.ToList())
        End Function
        ' GET: Comments by contact
        'TODO I'm here - From tutorial.
        Function CantactComments(ByVal id As Integer?) As ActionResult
            Dim filterComments = From s In db.TB_Comments Select s

            '            students = students.Where(s >= s.LastName.Contains(searchString)
            '                              || s.FirstMidName.Contains(searchString));
            filterComments = filterComments.Where(s >= s.ParentType = "Comments" || s.parentid=id)

            Return View(filterComments)
        End Function

        ' GET: Comments/Details/5
        Function Details(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim tB_Comments As TB_Comments = db.TB_Comments.Find(id)
            If IsNothing(tB_Comments) Then
                Return HttpNotFound()
            End If
            Return View(tB_Comments)
        End Function

        ' GET: Comments/Create
        Function Create() As ActionResult
            Return View()
        End Function

        ' POST: Comments/Create
        'Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        'más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="CommentID,Comment,ParentType,ParentId")> ByVal tB_Comments As TB_Comments) As ActionResult
            If ModelState.IsValid Then
                db.TB_Comments.Add(tB_Comments)
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(tB_Comments)
        End Function

        ' GET: Comments/Edit/5
        Function Edit(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim tB_Comments As TB_Comments = db.TB_Comments.Find(id)
            If IsNothing(tB_Comments) Then
                Return HttpNotFound()
            End If
            Return View(tB_Comments)
        End Function

        ' POST: Comments/Edit/5
        'Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        'más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(<Bind(Include:="CommentID,Comment,ParentType,ParentId")> ByVal tB_Comments As TB_Comments) As ActionResult
            If ModelState.IsValid Then
                db.Entry(tB_Comments).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(tB_Comments)
        End Function

        ' GET: Comments/Delete/5
        Function Delete(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim tB_Comments As TB_Comments = db.TB_Comments.Find(id)
            If IsNothing(tB_Comments) Then
                Return HttpNotFound()
            End If
            Return View(tB_Comments)
        End Function

        ' POST: Comments/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Integer) As ActionResult
            Dim tB_Comments As TB_Comments = db.TB_Comments.Find(id)
            db.TB_Comments.Remove(tB_Comments)
            db.SaveChanges()
            Return RedirectToAction("Index")
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub
    End Class
End Namespace
